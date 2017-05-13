using System.Threading.Tasks;
using AuthGuard.Api;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application;

namespace AuthGuard.SL.Notifications
{
    public interface INotificationsWorkflowService : IWorkflowService
    {
        Task<OperationResult> SendAddLocalProviderNotificationAsync(UserNameIm im);
        Task<OperationResult> SendTwoFactorNotificationAsync(LocalProviderIm im);
    }
}