using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Errors;

namespace AuthGuard.Services.Apps
{
    public interface IAppsService
    {
        Task<AppVm> GetAuthGuardApp();
        Task<AppVm> SearchAsync(string returnUrl);
        Task<(ExtendedAppVm App, OperationResult OperationResult)> PutAsync(Guid id, ExtendedAppIm im);
        Task<IEnumerable<ExtendedAppVm>> GetAllAsync();
        Task<ExtendedAppVm> GetAsync(Guid id);
    }
}