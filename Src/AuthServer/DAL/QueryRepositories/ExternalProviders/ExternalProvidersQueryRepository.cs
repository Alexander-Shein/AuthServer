using System.Collections.Generic;
using System.Threading.Tasks;
using DddCore.Contracts.DAL;
using DddCore.DAL.QueryStack.Dapper;
using Microsoft.Extensions.Options;

namespace AuthGuard.DAL.QueryRepositories.ExternalProviders
{
    public class ExternalProvidersQueryRepository : QueryRepositoryBase, IExternalProvidersQueryRepository
    {
        public ExternalProvidersQueryRepository(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<ExternalProviderDto>> GetAll()
        {
            var sql = "SELECT * FROM [dbo].[ExternalProvider];";
            return await GetListAsync<ExternalProviderDto>(sql);
        }

        public async Task<IEnumerable<SearchableExternalProviderDto>> GetSearchableAsync()
        {
            var sql = "SELECT * FROM [dbo].[ExternalProvider] WHERE [IsSearchable] = 1;";
            return await GetListAsync<SearchableExternalProviderDto>(sql);
        }
    }
}