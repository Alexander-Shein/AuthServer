using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.SL.Contracts.Models.Input.ApiServices;
using AuthGuard.SL.Contracts.Models.View.ApiServices;
using DddCore.Contracts.SL.Services.Application;
using DddCore.Contracts.SL.Services.Application.DomainStack.Crud.Async;

namespace AuthGuard.SL.Contracts.ApiServices
{
    public interface IApiResourcesWorkflowService :
        IWorkflowService,
        IDeleteAsync<Guid>,
        ICreateOrUpdateAsync<ApiResourceVm, Guid, ApiResourceIm>
    {
        Task<IEnumerable<ApiResourceVm>> GetApiResourcesAsync();
    }
}