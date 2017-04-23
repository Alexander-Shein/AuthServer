using System.Threading.Tasks;
using AuthGuard.Services.Support;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class SupportController : Controller
    {
        readonly ISupportWorkflowService supportWorkflowService;

        public SupportController(ISupportWorkflowService supportWorkflowService)
        {
            this.supportWorkflowService = supportWorkflowService;
        }

        [HttpPost("messages")]
        public async Task<IActionResult> PostAsync([FromBody] MessageIm im)
        {
            var result = await supportWorkflowService.SendMessage(im);

            if (result.OperationResult.IsNotSucceed)
            {
                return BadRequest(result.OperationResult.Errors);
            }

            return Ok(result.Message);
        }
    }
}