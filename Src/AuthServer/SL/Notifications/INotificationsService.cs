using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Infrastructure;

namespace AuthGuard.SL.Notifications
{
    public interface INotificationsService : IInfrastructureService
    {
        Task<OperationResult> SendMessageAsync(string userName, Template template, IDictionary<string, string> parameters);
    }
}