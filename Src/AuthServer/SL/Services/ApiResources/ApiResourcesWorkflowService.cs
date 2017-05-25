using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities.ApiResources;
using AuthGuard.DAL.QueryRepositories.ApiResources;
using AuthGuard.SL.Contracts.ApiServices;
using AuthGuard.SL.Contracts.Models.Input.ApiServices;
using AuthGuard.SL.Contracts.Models.View.ApiServices;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.Crosscutting.ObjectMapper;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.DAL.DomainStack;

namespace AuthGuard.SL.Services.ApiServices
{
    public class ApiResourcesWorkflowService : IApiResourcesWorkflowService
    {
        readonly IObjectMapper objectMapper;
        readonly IApiResourcesQueryRepository apiResourcesQueryRepository;
        readonly IUserContext<Guid> userContext;
        readonly IUnitOfWork unitOfWork;
        readonly IRepository<ApiResource, Guid> apiResourcesRepository;
        readonly IBusinessRulesValidatorFactory businessRulesValidatorFactory;

        public ApiResourcesWorkflowService(
            IObjectMapper objectMapper,
            IApiResourcesQueryRepository apiResourcesQueryRepository,
            IUserContext<Guid> userContext,
            IUnitOfWork unitOfWork,
            IRepository<ApiResource, Guid> apiResourcesRepository,
            IBusinessRulesValidatorFactory businessRulesValidatorFactory)
        {
            this.objectMapper = objectMapper;
            this.apiResourcesQueryRepository = apiResourcesQueryRepository;
            this.userContext = userContext;
            this.unitOfWork = unitOfWork;
            this.apiResourcesRepository = apiResourcesRepository;
            this.businessRulesValidatorFactory = businessRulesValidatorFactory;
        }

        public async Task<IEnumerable<ApiResourceVm>> GetApiResourcesAsync()
        {
            var dtos = await apiResourcesQueryRepository.GetApiResourcesAsync(userContext.Id);
            return dtos.Select(objectMapper.Map<ApiResourceVm>);
        }

        public async Task<(ApiResourceVm Vm, OperationResult OperationResult)> CreateOrUpdateAsync(Guid id, ApiResourceIm im)
        {
            var domain = await apiResourcesRepository.GetByIdAsync(id);

            /*var putResult = //await apiResourcesEntityService.PutAsync(id, im);
            if (putResult.OperationResult.IsNotSucceed)
            {
                return (null, putResult.OperationResult);
            }

            await unitOfWork.SaveAsync();
            return (objectMapper.Map<ApiResourceVm>(putResult.ApiResource), putResult.OperationResult);*/
            throw new NotImplementedException();
        }

        public Task<OperationResult> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
            /*var result = await apiResourcesEntityService.DeleteAsync(id);

            if (result.IsSucceed)
            {
                await unitOfWork.SaveAsync();
            }

            return result;*/
        }
    }
}