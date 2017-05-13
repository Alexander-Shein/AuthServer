using System.Threading.Tasks;
using AuthGuard.SL.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [Route("api/[controller]")]
    public class NotificationsController : Controller
    {
        readonly INotificationsWorkflowService notificationsWorkflowService;

        public NotificationsController(INotificationsWorkflowService notificationsWorkflowService)
        {
            this.notificationsWorkflowService = notificationsWorkflowService;
        }

        [Authorize]
        [HttpPost("new-local-provider")]
        public async Task<IActionResult> SendAddLocalProviderNotificationAsync([FromBody] UserNameIm im)
        {
            var result = await notificationsWorkflowService.SendAddLocalProviderNotificationAsync(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost("two-factor")]
        public async Task<IActionResult> SendTwoFactorNotificationAsync([FromBody] LocalProviderIm im)
        {
            var result = await notificationsWorkflowService.SendTwoFactorNotificationAsync(im);

            if (result.IsNotSucceed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}