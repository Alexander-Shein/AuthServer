using System.Threading.Tasks;
using AuthGuard.Services.Passwordless;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class PasswordlessController : Controller
    {
        readonly IPasswordlessService passwordlessService;

        public PasswordlessController(IPasswordlessService passwordlessService)
        {
            this.passwordlessService = passwordlessService;
        }

        [HttpPost("sign-up/link")]
        public async Task<IActionResult> SendSignUpLinkAsync([FromBody] CallbackUrlAndUserNameIm im)
        {
            var result = await passwordlessService.SendSignUpLinkAsync(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync([FromBody] CodeIm im)
        {
            var result = await passwordlessService.SignUpAsync(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost("log-in/link")]
        public async Task<IActionResult> SendLogInLinkAsync([FromBody] CallbackUrlAndUserNameIm im)
        {
            var result = await passwordlessService.SendLogInLinkAsync(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}