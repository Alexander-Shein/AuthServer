using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities.Identity.BusinessRules;
using AuthGuard.DAL.QueryRepositories.Identity.Dtos;
using DddCore.Contracts.DAL;
using DddCore.DAL.QueryStack.Dapper;
using IdentityServer4.Extensions;
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
    }

    public class IdentityResourceUniqueNameValidator : QueryRepositoryBase, IIdentityResourceUniqueNameValidator
    {
        public IdentityResourceUniqueNameValidator(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken)
        {
            var sql = @"
                SELECT CASE WHEN
                    EXISTS(SELECT TOP 1 [Name] FROM [dbo].[IdentityResource] WHERE [Name] = @Name)
                    THEN 0
                    ELSE 1
                END;";

            var result = await GetAsync<bool>(sql, new { Name = name });
            return result;
        }
    }

    public class IdentityClaimsExistValidator : QueryRepositoryBase, IIdentityClaimsExistValidator
    {
        public IdentityClaimsExistValidator(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<bool> AreIdentityClaimsExist(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            if (ids.IsNullOrEmpty()) return true;

            var sql = $@"
                SELECT
                    CASE WHEN CAST((SELECT COUNT([Id]) FROM [dbo].[IdentityClaim] WHERE [Id] IN ({String.Join(",", ids.Select(x => $"'{x}'"))})) AS INT) = @Count THEN 1
                    ELSE 0
                END";

            var result = await GetAsync<bool>(sql, new { Count = ids.Count() });
            return result;
        }
    }
}