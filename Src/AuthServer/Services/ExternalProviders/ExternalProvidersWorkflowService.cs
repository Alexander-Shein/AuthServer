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

        public async Task<IEnumerable<ExternalProviderVm>> GetAll()
        {
            var dtos = await externalProvidersQueryRepository.GetAll();
            return dtos.Select(x => new ExternalProviderVm
            {
                Id = x.Id,
                DisplayName = x.DisplayName,
                AuthenticationScheme = x.AuthenticationScheme
            });
        }
    }
}