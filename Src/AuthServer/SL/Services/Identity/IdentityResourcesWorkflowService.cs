using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.DAL.QueryRepositories.Identity;
using AuthGuard.SL.Contracts.Identity;
using AuthGuard.SL.Contracts.Models.Input.Identity;
using AuthGuard.SL.Contracts.Models.View.Identity;
using AutoMapper;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.DAL.DomainStack;

namespace AuthGuard.SL.Services.Identity
{
    public class IdentityResourcesWorkflowService : IIdentityResourcesWorkflowService
    {
        readonly IMapper mapper;
        readonly IIdentityResourcesQueryRepository identityResourcesQueryRepository;
        readonly IIdentityResourcesEntityService identityResourcesEntityService;
        readonly IUnitOfWork unitOfWork;

        public IdentityResourcesWorkflowService(
            IMapper mapper,
            IIdentityResourcesQueryRepository identityResourcesQueryRepository,
            IUnitOfWork unitOfWork,
            IIdentityResourcesEntityService identityResourcesEntityService)
        {
            this.mapper = mapper;
            this.identityResourcesQueryRepository = identityResourcesQueryRepository;
            this.unitOfWork = unitOfWork;
            this.identityResourcesEntityService = identityResourcesEntityService;
        }

        public async Task<IEnumerable<IdentityResourceVm>> GetIdentityResourcesAsync()
        {
            var dtos = await identityResourcesQueryRepository.GetIdentityResourcesAsync();
            return dtos.Select(mapper.Map<IdentityResourceVm>);
        }

        public async Task<IEnumerable<IdentityClaimVm>> GetIdentityClaimsAsync()
        {
            var dtos = await identityResourcesQueryRepository.GetIdentityClaimsAsync();
            return dtos.Select(mapper.Map<IdentityClaimVm>);
        }

        public async Task<(IdentityResourceVm IdentityResource, OperationResult OperationResult)> PutAsync(Guid id, IdentityResourceIm im)
        {
            var putResult = await identityResourcesEntityService.PutAsync(id, im);
            if (putResult.OperationResult.IsNotSucceed)
            {
                return (null, putResult.OperationResult);
            }

            await unitOfWork.SaveAsync();
            return (mapper.Map<IdentityResourceVm>(putResult.IdentityResource), putResult.OperationResult);
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var result = await identityResourcesEntityService.DeleteAsync(id);

            if (result.IsSucceed)
            {
                await unitOfWork.SaveAsync();
            }

            return result;
        }
    }
}