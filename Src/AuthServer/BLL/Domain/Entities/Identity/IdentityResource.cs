using System.Collections.Generic;
using AuthGuard.BLL.Domain.Entities.Common;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities.Identity
{
    public class IdentityResource : GuidAggregateRootEntityBase, IReadOnly, IEnabled, IRowVersion, IDisplayName
    {
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Emphasize { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsEnabled { get; set; }
        public byte[] Ts { get; set; }
        public ICollection<IdentityResourceClaim> Claims { get; set; }
    }
}