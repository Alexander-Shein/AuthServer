using System;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.DAL.Repositories.Security;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.SL.Services.Application.DomainStack;
using DddCore.SL.Services.Application.DomainStack;

namespace AuthGuard.SL.Security
{
    public class SecurityCodesService : EntityService<SecurityCode, Guid>, ISecurityCodesEntityService
    {
        public SecurityCodesService(ISecurityCodesRepository securityCodesRepository, IGuard guard) : base(securityCodesRepository, guard)
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