using System;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities
{
    public class Email : GuidAggregateRootEntityBase
    {
        public Guid EmailTemplateId { get; set; }
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsSent { get; set; }
        public DateTime CreatedAt { get; set; }

        public EmailTemplate EmailTemplate { get; set; }
    }
}