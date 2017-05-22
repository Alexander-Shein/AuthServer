using System;
using System.Collections.Generic;

namespace AuthGuard.DAL.QueryRepositories.ApiResources.Dtos
{
    public class ApiScopeDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Emphasize { get; set; }
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }

        public IEnumerable<ApiScopeClaimDto> UserClaims { get; set; }
    }
}