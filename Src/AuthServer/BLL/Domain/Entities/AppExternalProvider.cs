using System;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities
{
    public class AppExternalProvider : GuidEntityBase
    {
        public Guid AppId { get; set; }
        public Guid ExternalProviderId { get; set; }
        public ExternalProvider ExternalProvider { get; set; }
    }
}