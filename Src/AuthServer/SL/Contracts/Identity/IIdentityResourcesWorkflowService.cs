using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.SL.Contracts.Models.Input.Identity;
using AuthGuard.SL.Contracts.Models.View.Identity;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application;

namespace AuthGuard.SL.Contracts.Identity
{
    public interface IIdentityResourcesWorkflowService : IWorkflowService
    {
        Task<IEnumerable<IdentityResourceVm>> GetIdentityResourcesAsync();
        Task<IEnumerable<IdentityClaimVm>> GetIdentityClaimsAsync();

        Task<(IdentityResourceVm IdentityResource, OperationResult OperationResult)> PutAsync(Guid id, IdentityResourceIm im);
        Task<OperationResult> DeleteAsync(Guid id);
    }
}