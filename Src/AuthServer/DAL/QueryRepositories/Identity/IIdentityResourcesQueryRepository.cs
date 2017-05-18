using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.DAL.QueryRepositories.Identity.Dtos;
using DddCore.Contracts.DAL.QueryStack;

namespace AuthGuard.DAL.QueryRepositories.Identity
{
    public interface IIdentityResourcesQueryRepository : IQueryRepository
    {
        Task<IEnumerable<IdentityResourceDto>> GetIdentityResourcesAsync();
        Task<IEnumerable<IdentityClaimDto>> GetIdentityClaimsAsync();
    }
}