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
        public async Task<IActionResult> IsUserNameExistAsync(string userName)
        {
            var isExist = await usersService.IsUserNameExistAsync(userName);

            if (!isExist)
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
    }
}