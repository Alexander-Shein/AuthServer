using System.Threading.Tasks;
using AuthGuard.SL.Users;
using AuthGuard.SL.Users.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersWorkflowService usersService;

        public UsersController(IUsersWorkflowService usersService)
        {
            this.usersService = usersService;
        }

        [HttpHead]
        public async Task<IActionResult> IsUserNameExistsAsync(string userName)
        {
            var user = await usersService.GetUserByEmailOrPhoneAsync(userName);

            if (user == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var user = await usersService.GetCurrentUserAsync();
            return Ok(user);
        }

        [Authorize]
        [HttpPatch("me")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserIm im)
        {
            var result = await usersService.UpdateAsync(im);

            if (result.OperationResult.IsNotSucceed)
            {
                return BadRequest(result.OperationResult.Errors);
            }

            return Ok(result.User);
        }

        [Authorize]
        [HttpPost("me/notifications/new-local-provider")]
        public async Task<IActionResult> SendCodeToAddLocalProvider([FromBody] UserNameIm im)
        {
            var result = await usersService.SendCodeToAddLocalProvider(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut("me/providers/{provider}/confirmed")]
        public async Task<IActionResult> ConfirmAccountAsync([FromBody] ConfirmationCodeIm im, string provider)
        {
            var result = await usersService.ConfirmAccountAsync(new ConfirmAccountIm
            {
                Code = im.Code,
                Provider = provider
            });

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}