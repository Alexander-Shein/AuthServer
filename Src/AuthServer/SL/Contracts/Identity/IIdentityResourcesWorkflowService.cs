using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.SL.Contracts.Models.Input.Identity;
using AuthGuard.SL.Contracts.Models.View.Identity;
using DddCore.Contracts.SL.Services.Application;
using DddCore.Contracts.SL.Services.Application.DomainStack.Crud.Async;

namespace AuthGuard.SL.Contracts.Identity
{
    public interface IIdentityResourcesWorkflowService : IWorkflowService, IDeleteAsync<Guid>, ICreateOrUpdateAsync<IdentityResourceVm, Guid, IdentityResourceIm>, ICreateAsync<IdentityResourceVm, IdentityResourceIm>
    {
        Task<IEnumerable<IdentityResourceVm>> GetIdentityResourcesAsync();
        Task<IEnumerable<IdentityClaimVm>> GetIdentityClaimsAsync();
    }
}