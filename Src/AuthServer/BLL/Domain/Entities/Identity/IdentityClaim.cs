using System;
using AuthGuard.BLL.Domain.Entities.Common;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities.Identity
{
    public class IdentityClaim : GuidEntityBase, IReadOnly, IEnabled, IRowVersion
    {
        string description = String.Empty;
        public string Description
        {
            get => description;
            set => description = String.IsNullOrWhiteSpace(value) ? String.Empty : value.Trim();
        }

        string type;
        public string Type
        {
            get => type;
            set => type = value?.Trim()?.ToLower();
        }

        public bool IsReadOnly { get; set; }
        public bool IsEnabled { get; set; }
        public byte[] Ts { get; set; }
    }
}