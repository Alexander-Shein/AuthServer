using IdentityServerWithAspNetIdentity.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthServer.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpHead]
        public async Task<IActionResult> IsUserNameExists(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}
