using Idenitity_Provider.Persistence.Dtos.Auth;

namespace Identity_Provider.Service.Interfaces.Auth;

public interface IAuthService
{
    public Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto);
    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string email);
    public Task<(int Result, string Token)> VerifyRegisterAsync(string email, int code);
    public Task<(bool Result, string Token)> LoginAsyn(LoginDto dto);
}
