using Identity_Provider.Domain.Entities;
using Identity_Provider.Domain.Entities.Acconts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Provider.WebApi.Controllers.Providers
{
    [Route("api/")]
    [ApiController]
    public class MicrosoftAccountController : ControllerBase
    {
        [HttpGet("GetNames")]
        [AllowAnonymous]
        public IActionResult GetName()
        {
            return Ok(Data.NameList);
        }

        [HttpPost("PostNames")]
        [AllowAnonymous]
        public IActionResult PostName([FromForm] AccountModel nameModel)
        {
            Data.NameList.Add(nameModel);
            return Ok(Data.NameList);
        }
    }
}
