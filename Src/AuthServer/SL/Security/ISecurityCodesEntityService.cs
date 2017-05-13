using System;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using DddCore.Contracts.SL.Services.Application.DomainStack;

namespace AuthGuard.SL.Security
{
    public interface ISecurityCodesEntityService : IEntityService<SecurityCode, Guid>
    {
        void Insert(SecurityCode securityCode);
        void Delete(SecurityCode securityCode);
    }
}