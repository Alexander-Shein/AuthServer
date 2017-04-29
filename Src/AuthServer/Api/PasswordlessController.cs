using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthGuard.Api
{
    [Route("api/[controller]")]
    public class PasswordlessController : Controller
    {
        [HttpGet("sign-up")]
        public async Task<IActionResult> SignUpAsync()
        {
            return Ok();
        }

        [HttpGet("log-in")]
        public async Task<IActionResult> LogInAsync()
        {


            return Ok();
        }
    }
}
