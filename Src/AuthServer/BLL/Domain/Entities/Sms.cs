using System;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities
{
    public class Sms : GuidAggregateRootEntityBase
    {
        public Guid SmsTemplateId { get; set; }
        public string Message { get; set; }
        public string ToPhoneNumber { get; set; }
        public string FromPhoneNumber { get; set; }
        public bool IsSent { get; set; }
        public DateTime CreatedAt { get; set; }

        public SmsTemplate SmsTemplate { get; set; }
    }
}