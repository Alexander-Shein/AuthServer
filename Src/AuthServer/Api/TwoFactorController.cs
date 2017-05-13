using System.Threading.Tasks;
using AuthGuard.SL.TwoFactor;
using AuthGuard.SL.TwoFactor.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/two-factor")]
    public class TwoFactorController : Controller
    {
        readonly ITwoFactorsWorkflowService twoFactorsService;

        public TwoFactorController(ITwoFactorsWorkflowService twoFactorsService)
        {
            this.twoFactorsService = twoFactorsService;
        }

        [HttpGet("providers")]
        public async Task<IActionResult> GetTwoFactorProviders()
        {
            var result = await twoFactorsService.GetTwoFactorProvidersAsync();
            if (result.OperationResult.IsNotSucceed)
            {
                return BadRequest(result.OperationResult.Errors);
            }

            return Ok(result.Providers);
        }

        [Authorize]
        [HttpPost("verified")]
        public async Task<IActionResult> VerifyCode([FromBody] TwoFactorVerificationIm im)
        {
            var result = await twoFactorsService.VerifyCode(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}