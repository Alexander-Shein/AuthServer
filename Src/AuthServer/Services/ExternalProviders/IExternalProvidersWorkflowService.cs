using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.Services.Apps;
using DddCore.Contracts.SL.Services.Application;

namespace AuthGuard.Services.ExternalProviders
{
    public interface IExternalProvidersWorkflowService : IWorkflowService
    {
        Task<IEnumerable<ExternalProviderVm>> GetAll();
    }
}