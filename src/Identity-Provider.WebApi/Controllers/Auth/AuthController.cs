using Idenitity_Provider.Persistence.Dtos.Auth;
using Idenitity_Provider.Persistence.Validators;
using Idenitity_Provider.Persistence.Validators.Auth;
using Identity_Provider.Service.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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


        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userName = HttpContext.User.Identity.Name;
            // Return user information or perform actions based on the user's identity.

            return Ok(new { UserId = userId, UserName = userName });
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
    }
}
