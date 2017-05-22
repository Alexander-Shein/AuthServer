using System;
using System.Threading.Tasks;
using AuthGuard.SL.Contracts.ApiServices;
using AuthGuard.SL.Contracts.Models.Input.ApiServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [Route("api/api-resources")]
    public class ApiResourcesController : Controller
    {
        readonly IApiResourcesWorkflowService workflowService;

        public ApiResourcesController(IApiResourcesWorkflowService workflowService)
        {
            this.workflowService = workflowService;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] ApiResourceIm im)
        {
            var result = await workflowService.PutAsync(id, im);

            if (result.OperationResult.IsNotSucceed)
            {
                return BadRequest(result.OperationResult.Errors);
            }

            return Ok(result.IdentityResource);
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetIdentityResourcesAsync()
        {
            var result = await workflowService.GetApiResourcesAsync();
            return Ok(result);
        }
    }
}