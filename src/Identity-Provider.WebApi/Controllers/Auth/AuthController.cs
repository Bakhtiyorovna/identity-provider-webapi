using Idenitity_Provider.Persistence.Dtos.Auth;
using Idenitity_Provider.Persistence.Validators;
using Idenitity_Provider.Persistence.Validators.Auth;
using Identity_Provider.Service.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Provider.WebApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto dto)
        {
            UserRegisterValidator validations = new UserRegisterValidator();
            var resltvalid = validations.Validate(dto);
            if (resltvalid.IsValid)
            {
                var result = await _authService.RegisterAsync(dto);

                return Ok(new { result.Result, result.CachedMinutes });
            }
            else
                return BadRequest(resltvalid.Errors);

        }

        [HttpPost("register/send-code")]
        [AllowAnonymous]
        public async Task<IActionResult> SendCodeAsync(string email)
        {
            var valid = IdentityProviderValidator.IsValid(email);
            if (valid)
            {
                var result = await _authService.SendCodeForRegisterAsync(email);

                return Ok(new { result.Result, result.CachedVerificationMinutes });
            }
            else
                return BadRequest("Email invalid");

        }

        [HttpPost("register/verify")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyRegisterAsync([FromBody] VerifyUserDto dto)
        {
            var res = IdentityProviderValidator.IsValid(dto.IdentityProvider);
            if (res == false) return BadRequest("Email is invalid!");
            var srResult = await _authService.VerifyRegisterAsync(dto.IdentityProvider, dto.code);
            return Ok(new { srResult.Result, srResult.Token });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var res = IdentityProviderValidator.IsValid(dto.IdentityProvider);
            if (res == false)
                return BadRequest("Phone number is invalid!");

            var serviceResult = await _authService.LoginAsyn(dto);

            return Ok(new { serviceResult.Result, serviceResult.Token });
        }

        //[HttpPost("password/reset")]
        //public async Task<IActionResult> ResetPassword([FromBody] ForgotPassword forgot)
        //{
        //    var res = PhoneNumberValidator.IsValid(forgot.PhoneNumber);
        //    var password = PasswordValidator.IsStrongPassword(forgot.NewPassword);
        //    if (res == false)
        //        return BadRequest("Phone number is invalid!");
        //    else if (password.IsValid == false)
        //        return BadRequest(password.Message);

        //    var serviceResult = await _authService.ResetPasswordAsync(forgot);

        //    return Ok(new { serviceResult.Result, serviceResult.CachedMinutes });
        //}

        //[HttpPost("password/verify")]
        //public async Task<IActionResult> PasswordVerifyAsync([FromBody] VerfyUserDto verfyUser)
        //{
        //    var res = PhoneNumberValidator.IsValid(verfyUser.PhoneNumber);
        //    if (res == false) return BadRequest("Phone number is invalid!");
        //    var srResult = await _authService.VerifyResetPasswordAsync(verfyUser.PhoneNumber, verfyUser.Code);

        //    return Ok(new { srResult.Result, srResult.Token });
        //}

        //[HttpPost("token/verify")]
        //public async Task<IActionResult> CheckToken([FromBody] AuthorizationDto token)
        //{
        //    var requedt = await _authService.CheckTokenAsync(token);

        //    return Ok(requedt);
        //}
    }
}
