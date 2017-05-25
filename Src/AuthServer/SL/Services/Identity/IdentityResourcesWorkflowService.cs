using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities.Identity;
using AuthGuard.Common.Errors;
using AuthGuard.DAL.QueryRepositories.Identity;
using AuthGuard.SL.Contracts.Identity;
using AuthGuard.SL.Contracts.Models.Input.Identity;
using AuthGuard.SL.Contracts.Models.View.Identity;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.State;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.Crosscutting.ObjectMapper;
using DddCore.Contracts.DAL.DomainStack;

namespace AuthGuard.SL.Services.Identity
{
    public class IdentityResourcesWorkflowService : IIdentityResourcesWorkflowService
    {
        readonly IObjectMapper objectMapper;
        readonly IIdentityResourcesQueryRepository identityResourcesQueryRepository;
        readonly IUnitOfWork unitOfWork;
        readonly IBusinessRulesValidatorFactory businessRulesValidatorFactory;
        readonly IRepository<IdentityResource, Guid> identityResourcesRepository;

        public IdentityResourcesWorkflowService(
            IObjectMapper objectMapper,
            IIdentityResourcesQueryRepository identityResourcesQueryRepository,
            IUnitOfWork unitOfWork,
            IBusinessRulesValidatorFactory businessRulesValidatorFactory,
            IRepository<IdentityResource, Guid> identityResourcesRepository)
        {
            this.objectMapper = objectMapper;
            this.identityResourcesQueryRepository = identityResourcesQueryRepository;
            this.unitOfWork = unitOfWork;
            this.businessRulesValidatorFactory = businessRulesValidatorFactory;
            this.identityResourcesRepository = identityResourcesRepository;
        }

        public async Task<IEnumerable<IdentityResourceVm>> GetIdentityResourcesAsync()
        {
            var dtos = await identityResourcesQueryRepository.GetIdentityResourcesAsync();
            return dtos.Select(objectMapper.Map<IdentityResourceVm>);
        }

        public async Task<IEnumerable<IdentityClaimVm>> GetIdentityClaimsAsync()
        {
            var dtos = await identityResourcesQueryRepository.GetIdentityClaimsAsync();
            return dtos.Select(objectMapper.Map<IdentityClaimVm>);
        }

        public Task<(IdentityResourceVm Vm, OperationResult OperationResult)> CreateOrUpdateAsync(Guid id, IdentityResourceIm im)
        {
            throw new NotImplementedException();
            /*
            var putResult = await identityResourcesEntityService.PutAsync(id, im);
            if (putResult.OperationResult.IsNotSucceed)
            {
                return (null, putResult.OperationResult);
            }

            await unitOfWork.SaveAsync();
            return (objectMapper.Map<IdentityResourceVm>(putResult.IdentityResource), putResult.OperationResult);*/
        }

        public Task<OperationResult> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();

            /*
            var result = await identityResourcesEntityService.DeleteAsync(id);

            if (result.IsSucceed)
            {
                await unitOfWork.SaveAsync();
            }

            return result;*/
        }

        public async Task<(IdentityResourceVm Vm, OperationResult OperationResult)> CreateAsync(IdentityResourceIm im)
        {
            if (im == null) return (null,  OperationResult.Failed(AuthErrors.NullInputModel));

            var identityResource = objectMapper.Map<IdentityResource>(im);
            identityResource.WalkGraph(x => x.CrudState = CrudState.Added);

            var validationResult = await identityResource.ValidateAsync(businessRulesValidatorFactory);

            if (validationResult.IsNotSucceed) return (null, validationResult);

            identityResourcesRepository.Persist(identityResource);

            await unitOfWork.SaveAsync();

            return (objectMapper.Map<IdentityResourceVm>(identityResource), OperationResult.Succeed);
        }
    }
}