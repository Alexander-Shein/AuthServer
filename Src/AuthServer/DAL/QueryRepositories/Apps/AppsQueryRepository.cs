using System;
using System.Threading.Tasks;
using DddCore.Contracts.DAL;
using DddCore.DAL.QueryStack.Dapper;
using Microsoft.Extensions.Options;

namespace AuthGuard.DAL.QueryRepositories.Apps
{
    public class AppsQueryRepository : QueryRepositoryBase, IAppsQueryRepository
    {
        public AppsQueryRepository(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<bool> IsAppExist(string key)
        {
            var sql = "SELECT TOP 1 [Key] FROM [dbo].[App] WHERE [Key] = @Key;";

            var result = await GetAsync<string>(sql, new {Key = key});
            return !String.IsNullOrEmpty(result);
        }
    }
}