using System;

namespace AuthGuard.DAL.QueryRepositories.ApiResources.Dtos
{
    public class ApiScopeClaimDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}