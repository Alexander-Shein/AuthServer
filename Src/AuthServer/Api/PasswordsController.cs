using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AuthGuard.SL.Passwordless.Models;
using AuthGuard.SL.Passwords;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class PasswordsController : Controller
    {
        readonly IPasswordsService passwordsService;

        public PasswordsController(IPasswordsService passwordsService)
        {
            this.passwordsService = passwordsService;
        }

        [HttpPost]
        [Route("forgot")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] CallbackUrlAndUserNameIm im)
        {
            var result = await passwordsService.ForgotPasswordAsync(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost]
        [Route("reset")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordIm im)
        {
            var result = await passwordsService.ResetPasswordAsync(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("change")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordIm im)
        {
            var result = await passwordsService.ChangePasswordAsync(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPassword([FromBody] PasswordIm im)
        {
            var result = await passwordsService.AddPassword(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}