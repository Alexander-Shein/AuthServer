using System.Threading.Tasks;
using AuthGuard.Services.ExternalProviders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/external-providers")]
    public class ExternalProvidersController : Controller
    {
        readonly IExternalProvidersWorkflowService workflowService;

        public ExternalProvidersController(IExternalProvidersWorkflowService workflowService)
        {
            this.workflowService = workflowService;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var providers = await workflowService.GetAllAsync();
            return Ok(providers);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync(string filter)
        {
            var providers = await workflowService.SearchAsync(filter);
            return Ok(providers);
        }
    }
}