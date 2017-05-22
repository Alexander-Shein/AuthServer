using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.SL.Contracts.Models.Input.ApiServices;
using AuthGuard.SL.Contracts.Models.View.ApiServices;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application;

namespace AuthGuard.SL.Contracts.ApiServices
{
    public interface IApiResourcesWorkflowService : IWorkflowService
    {
        Task<IEnumerable<ApiResourceVm>> GetApiResourcesAsync();

        Task<(ApiResourceVm IdentityResource, OperationResult OperationResult)> PutAsync(Guid id, ApiResourceIm im);
        Task<OperationResult> DeleteAsync(Guid id);
    }
}