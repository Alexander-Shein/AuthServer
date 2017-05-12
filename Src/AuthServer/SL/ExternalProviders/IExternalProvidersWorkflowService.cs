using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.SL.Apps;
using DddCore.Contracts.SL.Services.Application;

namespace AuthGuard.SL.ExternalProviders
{
    public interface IExternalProvidersWorkflowService : IWorkflowService
    {
        Task<IEnumerable<ExternalProviderVm>> GetAllAsync();
        Task<IEnumerable<ExternalProviderVm>> SearchAsync(string filter);
    }
}