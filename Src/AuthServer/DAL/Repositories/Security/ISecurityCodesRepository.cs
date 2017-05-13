using System;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using DddCore.Contracts.DAL.DomainStack;

namespace AuthGuard.DAL.Repositories.Security
{
    public interface ISecurityCodesRepository : IRepository<SecurityCode, Guid>
    {
        Task<SecurityCode> ReadByCodeAsync(int code);
    }
}