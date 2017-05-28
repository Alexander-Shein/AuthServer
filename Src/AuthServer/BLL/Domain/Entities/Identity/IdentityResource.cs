using System;
using System.Collections.Generic;
using System.Linq;
using AuthGuard.BLL.Domain.Entities.Common;
using DddCore.BLL.Domain.Entities.GuidEntities;
using DddCore.Contracts.BLL.Domain.Entities.State;

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
            set
            {
                var newName = value?.Trim()?.ToLower();

                if (!String.IsNullOrEmpty(name) && name != newName)
                {
                    IsNameChanged = true;
                }

                name = newName;
            }
        }

        public bool IsNameChanged { get; private set; }

        public bool Emphasize { get; set; }
        public bool IsRequired { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsEnabled { get; set; }
        public byte[] Ts { get; set; }

        private List<IdentityResourceClaim> claims = new List<IdentityResourceClaim>();

        public ICollection<IdentityResourceClaim> Claims
        {
            get => claims;
            set => claims = value?.DistinctBy(x => x.IdentityClaimId).ToList();
        }

        public void Delete()
        {
            WalkGraph(x => x.CrudState = CrudState.Deleted);
        }

        public void DeleteClaims()
        {
            foreach (var identityResourceClaim in Claims)
            {
                identityResourceClaim.CrudState = CrudState.Deleted;
            }
        }

        public void Update()
        {
            foreach (var identityResourceClaim in Claims)
            {
                identityResourceClaim.CrudState = CrudState.Added;
            }

            CrudState = CrudState.Modified;
        }

        public void Create()
        {
            WalkGraph(x => x.CrudState = CrudState.Added);
        }
    }
}