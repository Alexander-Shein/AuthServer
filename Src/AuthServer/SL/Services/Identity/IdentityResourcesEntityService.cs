using System;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities.Identity;
using AuthGuard.SL.Contracts.Models.Input.Identity;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.DAL.DomainStack;
using DddCore.Contracts.SL.Services.Application.DomainStack;
using DddCore.SL.Services.Application.DomainStack;

namespace AuthGuard.SL.Services.Identity
{
    public class IdentityResourcesEntityService : EntityService<IdentityResource, Guid>, IIdentityResourcesEntityService
    {
        public IdentityResourcesEntityService(
            IRepository<IdentityResource, Guid> repository,
            IGuard guard,
            IBusinessRulesValidatorFactory businessRulesValidatorFactory,
            IDomainEventDispatcher domainEventDispatcher)
            : base(repository, guard, businessRulesValidatorFactory, domainEventDispatcher)
        {
        }

        public Task<(IdentityResource IdentityResource, OperationResult OperationResult)> PutAsync(Guid id, IdentityResourceIm im)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}