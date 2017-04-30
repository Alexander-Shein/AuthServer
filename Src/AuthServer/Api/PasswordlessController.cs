using System.Threading.Tasks;
using AuthGuard.Services.Passwordless;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthGuard.Api
{
    [Route("api/[controller]")]
    public class PasswordlessController : Controller
    {
        readonly IPasswordlessService passwordlessService;

        public PasswordlessController(IPasswordlessService passwordlessService)
        {
            this.passwordlessService = passwordlessService;
        }

        [HttpGet("sign-up")]
        public async Task<IActionResult> SignUpAsync([FromBody] UserNameIm im)
        {
            await passwordlessService.SendSignUpLinkAsync(im);
            return Ok();
        }

        [HttpGet("log-in")]
        public async Task<IActionResult> LogInAsync([FromBody] UserNameIm im)
        {
            await passwordlessService.SendLogInLinkAsync(im);
            return Ok();
        }
    }
}
