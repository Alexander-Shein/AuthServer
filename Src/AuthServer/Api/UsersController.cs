using System;
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
            var user = await usersService.UpdateAsync(HttpContext.User, im);
            return Ok(user);
        }

        [HttpGet]
        [Route("{userId:Guid}/providers/{provider}/confirmed")]
        public async Task<IActionResult> ConfirmAccountAsync(Guid userId, string code, string provider, string redirectUrl)
        {
            var result = await usersService.ConfirmAccountAsync(new ConfirmAccountIm
            {
                Code = code,
                Provider = provider,
                UserId = userId
            });

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Redirect(redirectUrl);
        }

        [HttpPut]
        [Authorize]
        [Route("me/providers/{provider}/confirmed")]
        public async Task<IActionResult> ConfirmAccountAsync([FromBody] ConfirmationCodeIm im, string provider)
        {
            var user = await usersService.GetCurrentUserAsync(HttpContext.User);

            var result = await usersService.ConfirmAccountAsync(new ConfirmAccountIm
            {
                Code = im.Code,
                Provider = provider,
                UserId = user.Id
            });

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}