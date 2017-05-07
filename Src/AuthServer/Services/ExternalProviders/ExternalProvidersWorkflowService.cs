using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.DAL.QueryRepositories.ExternalProviders;
using AuthGuard.Services.Apps;

namespace AuthGuard.Services.ExternalProviders
{
    public class ExternalProvidersWorkflowService : IExternalProvidersWorkflowService
    {
        readonly IExternalProvidersQueryRepository externalProvidersQueryRepository;

        public ExternalProvidersWorkflowService(IExternalProvidersQueryRepository externalProvidersQueryRepository)
        {
            this.externalProvidersQueryRepository = externalProvidersQueryRepository;
        }

        public async Task<IEnumerable<ExternalProviderVm>> GetAllAsync()
        {
            var dtos = await externalProvidersQueryRepository.GetAll();
            return dtos.Select(x => new ExternalProviderVm
            {
                Id = x.Id,
                DisplayName = x.DisplayName,
                AuthenticationScheme = x.AuthenticationScheme
            });
        }

        public async Task<IEnumerable<ExternalProviderVm>> SearchAsync(string filter)
        {
            if (String.Equals(filter, "searchable"))
            {
                var dtos = await externalProvidersQueryRepository.GetSearchableAsync();
                return dtos.Select(x => new SearchableExternalProviderVm
                {
                    Id = x.Id,
                    DisplayName = x.DisplayName,
                    AuthenticationScheme = x.AuthenticationScheme,
                    Matches = x.Patterns.Split(';')
                });
            }

            return await GetAllAsync();
        }
    }
}