using System;
using DddCore.BLL.Domain.Entities.GuidEntities;
using DddCore.Contracts.BLL.Domain.Entities.Audit.At;

namespace AuthGuard.BLL.Domain.Entities
{
    public class Email : GuidAggregateRootEntityBase, ICreatedAt
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