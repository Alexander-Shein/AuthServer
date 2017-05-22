using System.Collections.Generic;

namespace AuthGuard.DAL.QueryRepositories.ApiResources.Dtos
{
    public class ApiResourceDto
    {
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public string DisplayName { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }

        public IEnumerable<ApiScopeDto> Scopes { get; set; }
    }
}