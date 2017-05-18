using System;
using System.Collections.Generic;

namespace AuthGuard.DAL.QueryRepositories.Identity.Dtos
{
    public class IdentityResourceDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Emphasize { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool IsEnabled { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public bool IsReadOnly { get; set; }
        public IEnumerable<IdentityClaimDto> Claims { get; set; }
    }
}