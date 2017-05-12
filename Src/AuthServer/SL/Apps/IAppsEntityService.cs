using System;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application.DomainStack;

namespace AuthGuard.SL.Apps
{
    public interface IAppsEntityService : IEntityService<App, Guid>
    {
        Task<(App App, OperationResult OperationResult)> PutAsync(Guid id, ExtendedAppIm im);
        Task<OperationResult> DeleteAsync(Guid id);
    }
}