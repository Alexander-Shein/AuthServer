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

        public async Task<(IdentityResourceVm Vm, OperationResult OperationResult)> CreateOrUpdateAsync(Guid id, IdentityResourceIm im)
        {
            if (im == null) return (null, OperationResult.Failed(AuthErrors.NullInputModel));

            var identityResource = identityResourcesRepository.GetById(id);
            if (identityResource == null) return await CreateAsync(im);
            identityResource.DeleteClaims();
            identityResourcesRepository.Persist(identityResource);

            objectMapper.Map(im, identityResource);
            identityResource.Update();

            var validationResult = identityResource.Validate(businessRulesValidatorFactory);
            if (validationResult.IsNotSucceed) return (null, validationResult);

            identityResourcesRepository.Persist(identityResource);
            await unitOfWork.SaveAsync();

            return (objectMapper.Map<IdentityResourceVm>(identityResource), OperationResult.Succeed);
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var identityResource = identityResourcesRepository.GetById(id);
            if (identityResource == null) return OperationResult.Succeed;

            identityResource.Delete();
            var validationResult = identityResource.Validate(businessRulesValidatorFactory);
            if (validationResult.IsNotSucceed) return validationResult;

            identityResourcesRepository.Persist(identityResource);
            await unitOfWork.SaveAsync();
            return OperationResult.Succeed;
        }

        public async Task<(IdentityResourceVm Vm, OperationResult OperationResult)> CreateAsync(IdentityResourceIm im)
        {
            if (im == null) return (null,  OperationResult.Failed(AuthErrors.NullInputModel));

            var identityResource = objectMapper.Map<IdentityResource>(im);
            identityResource.Create();

            var validationResult = identityResource.Validate(businessRulesValidatorFactory);
            if (validationResult.IsNotSucceed) return (null, validationResult);

            identityResourcesRepository.Persist(identityResource);
            await unitOfWork.SaveAsync();

            return (objectMapper.Map<IdentityResourceVm>(identityResource), OperationResult.Succeed);
        }
    }
}