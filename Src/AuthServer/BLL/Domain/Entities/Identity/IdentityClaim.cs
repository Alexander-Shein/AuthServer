using AuthGuard.BLL.Domain.Entities.Common;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities.Identity
{
    public class IdentityClaim : GuidEntityBase, IReadOnly, IEnabled, IRowVersion
    {
        public string Type { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsEnabled { get; set; }
        public byte[] Ts { get; set; }
    }
}