using System;
using System.Threading.Tasks;
using AuthGuard.SL.Contracts.Identity;
using AuthGuard.SL.Contracts.Models.Input.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [Route("api/identity-resources")]
    public class IdentityResourcesController : Controller
    {
        readonly IIdentityResourcesWorkflowService workflowService;

        public IdentityResourcesController(IIdentityResourcesWorkflowService workflowService)
        {
            this.workflowService = workflowService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] IdentityResourceIm im)
        {
            var result = await workflowService.CreateOrUpdateAsync(id, im);

            if (result.OperationResult.IsNotSucceed)
            {
                return BadRequest(result.OperationResult.Errors);
            }

            return Ok(result.Vm);
        }
        
        [HttpPost("")]
        public async Task<IActionResult> PostAsync([FromBody] IdentityResourceIm im)
        {
            var result = await workflowService.CreateAsync(im);

            if (result.OperationResult.IsNotSucceed)
            {
                return BadRequest(result.OperationResult.Errors);
            }

            return Created(String.Empty, result.Vm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletAsync(Guid id)
        {
            var result = await workflowService.DeleteAsync(id);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetIdentityResourcesAsync()
        {
            var result = await workflowService.GetIdentityResourcesAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("claims")]
        public async Task<IActionResult> GetIdentityClaimsAsync()
        {
            var result = await workflowService.GetIdentityClaimsAsync();
            return Ok(result);
        }
    }
}