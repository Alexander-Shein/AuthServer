using System;
using System.Collections.Generic;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities
{
    public class SmsTemplate : GuidAggregateRootEntityBase
    {
        public Template Template { get; set; }
        public string MessageTemplate { get; set; }
        public string FromPhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public Sms Render(string toPhoneNumber, IDictionary<string, string> parameters)
        {
            var result = new Sms
            {
                Message = ApplyParameters(MessageTemplate, parameters),
                CreatedAt = DateTime.UtcNow,
                FromPhoneNumber = FromPhoneNumber,
                ToPhoneNumber = toPhoneNumber
            };

            return result;
        }

        private string ApplyParameters(string template, IDictionary<string, string> parameters)
        {
            if (parameters == null) return template;

            foreach (var parameter in parameters)
            {
                template = template.Replace($"{{{parameter.Key}}}", parameter.Value);
            }

            return template;
        }
    }
}