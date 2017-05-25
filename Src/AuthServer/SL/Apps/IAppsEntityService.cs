using System;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Errors;

namespace AuthGuard.SL.Apps
{
    public interface IAppsEntityService
    {
        Task<(App App, OperationResult OperationResult)> PutAsync(Guid id, ExtendedAppIm im);
        Task<OperationResult> DeleteAsync(Guid id);
    }
}