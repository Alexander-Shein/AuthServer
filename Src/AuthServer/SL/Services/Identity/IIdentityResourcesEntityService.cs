using System;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities.Identity;
using AuthGuard.SL.Contracts.Models.Input.Identity;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application.DomainStack;

namespace AuthGuard.SL.Services.Identity
{
    public interface IIdentityResourcesEntityService : IEntityService<IdentityResource, Guid>
    {
        Task<(IdentityResource IdentityResource, OperationResult OperationResult)> PutAsync(Guid id, IdentityResourceIm im);
        Task<OperationResult> DeleteAsync(Guid id);
    }
}