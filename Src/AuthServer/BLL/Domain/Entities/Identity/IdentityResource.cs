using System;
using System.Collections.Generic;
using AuthGuard.BLL.Domain.Entities.Common;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities.Identity
{
    public class IdentityResource : GuidAggregateRootBase, IReadOnly, IEnabled, IRowVersion, IDisplayName
    {
        string description = String.Empty;
        public string Description
        {
            get => description;
            set => description = String.IsNullOrWhiteSpace(value) ? String.Empty : value.Trim();
        }

        string displayName;
        public string DisplayName
        {
            get => displayName;
            set => displayName = value?.Trim();
        }

        string name;
        public string Name
        {
            get => name;
            set => name = value?.Trim()?.ToLower();
        }

        public bool Emphasize { get; set; }
        public bool IsRequired { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsEnabled { get; set; }
        public byte[] Ts { get; set; }
        public ICollection<IdentityResourceClaim> Claims { get; set; }
    }
}