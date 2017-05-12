using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application;

namespace AuthGuard.SL.Apps
{
    public interface IAppsWorkflowService : IWorkflowService
    {
        Task<AppVm> GetAuthGuardApp();
        Task<AppVm> SearchAsync(string returnUrl);
        Task<(ExtendedAppVm App, OperationResult OperationResult)> PutAsync(Guid id, ExtendedAppIm im);
        Task<IEnumerable<ExtendedAppVm>> GetAllAsync();
        Task<ExtendedAppVm> GetAsync(Guid id);
        Task<bool> IsAppExistAsync(string key);
        Task<OperationResult> DeleteAsync(Guid id);
    }
}