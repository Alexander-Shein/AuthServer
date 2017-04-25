using System;
using System.Threading.Tasks;
using AuthGuard.Services.Apps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class AppsController : Controller
    {
        readonly IAppsService appsService;

        public AppsController(IAppsService appsService)
        {
            this.appsService = appsService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync(string returnUrl)
        {
            if (String.IsNullOrWhiteSpace(returnUrl))
            {
                return Ok(await appsService.GetAuthGuardApp());
            }

            return Ok(await appsService.SearchAsync(returnUrl));
        }

        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> PostAsync([FromBody] ExtendedAppIm im)
        {
            return await PutAsync(Guid.NewGuid(), im);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] ExtendedAppIm im)
        {
            var result = await appsService.PutAsync(id, im);

            if (result.OperationResult.IsNotSucceed)
            {
                return BadRequest(result.OperationResult.Errors);
            }

            return Ok(result.App);
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await appsService.GetAllAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await appsService.GetAsync(id);
            return Ok(result);
        }
    }
}