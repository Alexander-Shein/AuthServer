using System;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.DAL.Repositories.Security;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.SL.Services.Application.DomainStack;
using DddCore.SL.Services.Application.DomainStack;

namespace AuthGuard.SL.Security
{
    public class SecurityCodesEntityService : EntityService<SecurityCode, Guid>, ISecurityCodesEntityService
    {
        public SecurityCodesEntityService(
            ISecurityCodesRepository securityCodesRepository,
            IGuard guard,
            IBusinessRulesValidatorFactory businessRulesValidatorFactory,
            IDomainEventDispatcher domainEventDispatcher)
            : base(securityCodesRepository, guard, businessRulesValidatorFactory, domainEventDispatcher)
        {
        }

        public void Insert(SecurityCode securityCode)
        {
            securityCode.WalkGraph(x => x.CrudState = CrudState.Added);
            ValidateAndPersist(securityCode);
        }

        public void Delete(SecurityCode securityCode)
        {
            if (securityCode == null) return;
            securityCode.WalkGraph(x => x.CrudState = CrudState.Deleted);
            ValidateAndPersist(securityCode);
        }
    }
}