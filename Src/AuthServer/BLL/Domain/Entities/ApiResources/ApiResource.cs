using System;
using System.Collections.Generic;
using AuthGuard.BLL.Domain.Entities.Common;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities.ApiResources
{
    public class ApiResource : GuidAggregateRootEntityBase, IEnabled, IRowVersion, IDisplayName
    {
        public Guid OwnerUserId { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public string DisplayName { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public byte[] Ts { get; set; }

        public ICollection<ApiScope> Scopes { get; set; }
    }
}