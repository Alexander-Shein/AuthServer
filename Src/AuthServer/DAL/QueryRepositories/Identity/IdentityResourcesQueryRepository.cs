using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AuthGuard.DAL.QueryRepositories.Identity.Dtos;
using DddCore.Contracts.DAL;
using DddCore.DAL.QueryStack.Dapper;
using Microsoft.Extensions.Options;

namespace AuthGuard.DAL.QueryRepositories.Identity
{
    public class IdentityResourcesQueryRepository : QueryRepositoryBase, IIdentityResourcesQueryRepository
    {
        public IdentityResourcesQueryRepository(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings)
        {
        }

        public Task<IEnumerable<IdentityResourceDto>> GetIdentityResourcesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<IdentityClaimDto>> GetIdentityClaimsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}