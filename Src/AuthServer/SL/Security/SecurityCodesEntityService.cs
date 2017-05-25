using AuthGuard.BLL.Domain.Entities;
using AuthGuard.DAL.Repositories.Security;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.State;
using DddCore.Contracts.SL.Services.Infrastructure;

namespace AuthGuard.SL.Security
{
    public class SecurityCodesEntityService : ISecurityCodesEntityService, IInfrastructureService
    {
        readonly IBusinessRulesValidatorFactory businessRulesValidatorFactory;
        readonly ISecurityCodesRepository securityCodesRepository;

        public SecurityCodesEntityService(
            ISecurityCodesRepository securityCodesRepository,
            IBusinessRulesValidatorFactory businessRulesValidatorFactory)
        {
            this.securityCodesRepository = securityCodesRepository;
            this.businessRulesValidatorFactory = businessRulesValidatorFactory;
        }

        public void Insert(SecurityCode securityCode)
        {
            securityCode.WalkGraph(x => x.CrudState = CrudState.Added);

            if (securityCode.Validate(businessRulesValidatorFactory).IsSucceed)
            {
                securityCodesRepository.Persist(securityCode);
            }
        }

        public void Delete(SecurityCode securityCode)
        {
            if (securityCode == null) return;
            securityCode.WalkGraph(x => x.CrudState = CrudState.Deleted);

            if (securityCode.Validate(businessRulesValidatorFactory).IsSucceed)
            {
                securityCodesRepository.Persist(securityCode);
            }
        }
    }
}