using System.Collections.Generic;
using AuthGuard.BLL.Domain.Entities.Common;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities.ApiResources
{
    public class ApiScope : GuidEntityBase, IEnabled, IRowVersion, IDisplayName
    {
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Emphasize { get; set; }
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public byte[] Ts { get; set; }

        public ICollection<ApiScopeClaim> UserClaims { get; set; }
    }
}