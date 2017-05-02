using System.Threading.Tasks;
using AuthGuard.Services.Users;
using AuthGuard.Services.Users.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
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

        [HttpGet]
        [Authorize]
        [Route("me")]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var user = await usersService.GetCurrentUserAsync(HttpContext.User);
            return Ok(user);
        }

        [HttpPatch]
        [Authorize]
        [Route("me")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserIm im)
        {
            var result = await usersService.UpdateAsync(HttpContext.User, im);

            if (result.OperationResult.IsNotSucceed)
            {
                return BadRequest(result.OperationResult.Errors);
            }

            return Ok(result.User);
        }

        [HttpPost]
        [Authorize]
        [Route("me/notifications/new-local-provider")]
        public async Task<IActionResult> SendCodeToAddLocalProvider([FromBody] UserNameIm im)
        {
            var result = await usersService.SendCodeToAddLocalProvider(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut]
        [Route("me/providers/{provider}/confirmed")]
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