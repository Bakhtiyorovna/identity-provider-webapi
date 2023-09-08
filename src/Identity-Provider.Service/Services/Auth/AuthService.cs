using AutoMapper;
using Idenitity_Provider.Persistence.Dtos.Auth;
using Idenitity_Provider.Persistence.Dtos.Notifications;
using Identity_Provider.Application.Exceptions.Auth;
using Identity_Provider.Application.Exceptions.Users;
using Identity_Provider.DataAccess.Interfaces.Users;
using Identity_Provider.DataAccess.ViewModels.Users;
using Identity_Provider.Domain.Entities.Users;
using Identity_Provider.Service.Common.Helpers;
using Identity_Provider.Service.Interfaces.Auth;
using Identity_Provider.Service.Interfaces.Notifications;
using Identity_Provider.Service.Security;
using Microsoft.Extensions.Caching.Memory;

namespace Identity_Provider.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IEmailSender _emailSender;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private const int CACHED_FOR_MINUTS_REGISTER = 60;
    private const int CACHED_FOR_MINUTS_VEFICATION = 5;

    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
#pragma warning disable
    public AuthService(IMemoryCache memoryCache, IUserRepository userRepository,
        IEmailSender emailsender, ITokenService tokenService)
    {
        this._memoryCache = memoryCache;
        this._emailSender = emailsender;
        this._userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.IdentityProvider);
        if (user is not null )
            throw new UserAlreadyExsistExceptions();

        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.IdentityProvider, out RegisterDto registrDto))
        {
            registrDto.IdentityProvider = registrDto.IdentityProvider;
            _memoryCache.Remove(dto.IdentityProvider);
        }
        else
        {
            _memoryCache.Set(REGISTER_CACHE_KEY + dto.IdentityProvider, dto, TimeSpan.FromMinutes(CACHED_FOR_MINUTS_REGISTER));
        }
        return (Result: true, CachedMinutes: CACHED_FOR_MINUTS_REGISTER);
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string email)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + email, out RegisterDto registrDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();
            verificationDto.Code = CodeGenerator.GenerateRandomNumber();
            _memoryCache.Set(email, verificationDto, TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));

            // email sende begin
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + email, out VerificationDto oldVerifcationDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + email);
            }
            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + email, verificationDto,
                TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));
            // email sender end 
            EmailMessage smsMessage = new EmailMessage();
            smsMessage.Title = "My Platform";
            smsMessage.Content = "Your verification code : " + verificationDto.Code;
            smsMessage.Recipent = email;
            var result = await _emailSender.SenderAsync(smsMessage);
            if (result is true) return (Result: true, CachedVerificationMinutes: CACHED_FOR_MINUTS_VEFICATION);
            else return (Result: false, CachedMinutes: 0);
        }
        else
        {
            throw new UserExpiredException();
        }
    }

    public async Task<(int Result, string Token)> VerifyRegisterAsync(string email, int code)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + email, out RegisterDto registerDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + email, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestsException();
                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterToDatabaseAsync(registerDto);

                    return (Result: dbResult, Token: "");
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + email);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + email, verificationDto,
                        TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));

                    return (Result: 0, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();
        }
        else throw new UserExpiredException();
    }

    private async Task<int> RegisterToDatabaseAsync(RegisterDto registerDto)
    {
        User user = new User();
        var hasher = PasswordHasher.Hash(registerDto.Password);
        user.PasswordHash = hasher.Hash;
        user.Salt = hasher.Salt;

        var config = new MapperConfiguration(cnfg =>
        {
            cnfg.CreateMap<RegisterDto, User>();
        });

        var FirsMapping = new Mapper(config);

        var dbResult = await _userRepository.CreateAsync(user);

        return dbResult;
    }

    public async Task<(bool Result, string Token)> LoginAsyn(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.IdentityProvider);
        if (user is null) throw new UserNotFoundExceptions();

        var hasherResult = PasswordHasher.Verify(dto.Password, user.Salt, user.PasswordHash);
        if (hasherResult == false) throw new PasswordNotMatchException();

        UserViewModel userViewModel = new UserViewModel()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IdentityProvider=user.IdentityProvider,
            Confirm=user.Confirm,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
        };

        string token = _tokenService.GenereateToken(userViewModel);

        return (Result: true, Token: token);
    }
}
