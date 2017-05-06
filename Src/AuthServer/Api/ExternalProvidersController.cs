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
        public async Task<IActionResult> GetAll()
        {
            var providers = await workflowService.GetAll();
            return Ok(providers);
        }
    }
}