using AuthServer.Services.Users.Models.Input;
using IdentityServerWithAspNetIdentity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Api
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsUserNameExistsAsync(string userName)
        {
            var user = await usersService.GetUserByEmailOrPhoneAsync(userName);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("me")]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var user = await usersService.GetCurrentUserAsync(HttpContext.User);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPatch]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("me")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserIm im)
        {
            var user = await usersService.UpdateAsync(HttpContext.User, im);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpGet]
        [Route("{userId:Guid}")]
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
                redirectUrl += $";error={result.Errors.First().Description}";
            }

            return Redirect(redirectUrl);
        }
    }
}