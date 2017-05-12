using System;
using System.Threading.Tasks;
using AuthGuard.SL.Apps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class AppsController : Controller
    {
        readonly IAppsWorkflowService appsWorkflowService;

        public AppsController(IAppsWorkflowService appsWorkflowService)
        {
            this.appsWorkflowService = appsWorkflowService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync(string returnUrl)
        {
            if (String.IsNullOrWhiteSpace(returnUrl))
            {
                return Ok(await appsWorkflowService.GetAuthGuardApp());
            }

            return Ok(await appsWorkflowService.SearchAsync(returnUrl));
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
            var result = await appsWorkflowService.PutAsync(id, im);

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
            var result = await appsWorkflowService.GetAllAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await appsWorkflowService.GetAsync(id);
            return Ok(result);
        }

        [Authorize]
        [HttpHead("")]
        public async Task<IActionResult> IsAppExistAsync(string key)
        {
            var isExist = await appsWorkflowService.IsAppExistAsync(key);
            if (!isExist)
            {
                return NotFound();
            }

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await appsWorkflowService.DeleteAsync(id);
            return Ok();
        }
    }
}