using System.Threading.Tasks;
using DddCore.Contracts.BLL.Errors;

namespace AuthGuard.Services.Support
{
    public interface ISupportWorkflowService
    {
        Task<(MessageVm Message, OperationResult OperationResult)> SendMessage(MessageIm im);
    }
}