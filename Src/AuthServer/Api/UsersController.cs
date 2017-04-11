using AuthServer.Services.Users.Models.Input;
using IdentityServerWithAspNetIdentity.Services;
using IdentityServerWithAspNetIdentity.Services.Users.Models.View;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
        [Route("me")]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            return Ok(new UserVm
            {
                Email = "alex@live.com",
                HasPassword = true,
                IsTwoFactorEnabled = true,
                PhoneNumber = "375259065234",
                ExternalProviders = Enumerable.Empty<UserExternalProviderVm>()
            });

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
        [Route("me")]
        public async Task<IActionResult> Update(UserIm im)
        {
            var user = await usersService.Update(HttpContext.User, im);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }
    }
}