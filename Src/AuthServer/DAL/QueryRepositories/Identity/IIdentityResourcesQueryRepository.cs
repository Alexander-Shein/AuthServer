using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities.Identity.BusinessRules;
using AuthGuard.DAL.QueryRepositories.Identity.Dtos;
using DddCore.Contracts.DAL.QueryStack;

namespace AuthGuard.DAL.QueryRepositories.Identity
{
    public interface IIdentityResourcesQueryRepository : IQueryRepository, IIdentityResourceUniqueName
    {
        Task<IEnumerable<IdentityResourceDto>> GetIdentityResourcesAsync();
        Task<IEnumerable<IdentityClaimDto>> GetIdentityClaimsAsync();
    }
}