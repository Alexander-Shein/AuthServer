using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.DAL.QueryRepositories.ApiResources.Dtos;
using DddCore.Contracts.DAL;
using DddCore.DAL.QueryStack.Dapper;
using Microsoft.Extensions.Options;

namespace AuthGuard.DAL.QueryRepositories.ApiResources
{
    public class ApiResourcesQueryRepository : QueryRepositoryBase, IApiResourcesQueryRepository
    {
        public ApiResourcesQueryRepository(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings)
        {
        }

        public Task<IEnumerable<ApiResourceDto>> GetApiResourcesAsync(Guid ownerId)
        {
            throw new NotImplementedException();
        }
    }
}