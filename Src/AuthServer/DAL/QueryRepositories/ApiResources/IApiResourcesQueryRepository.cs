using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.DAL.QueryRepositories.ApiResources.Dtos;
using DddCore.Contracts.DAL.QueryStack;

namespace AuthGuard.DAL.QueryRepositories.ApiResources
{
    public interface IApiResourcesQueryRepository : IQueryRepository
    {
        Task<IEnumerable<ApiResourceDto>> GetApiResourcesAsync(Guid ownerId);
    }
}