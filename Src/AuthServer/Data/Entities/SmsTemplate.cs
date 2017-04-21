using System;
using System.Collections.Generic;

namespace AuthGuard.Data.Entities
{
    public class SmsTemplate
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public string MessageTemplate { get; set; }
        public string FromPhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public Template Template { get; set; }

        public Sms Render(IDictionary<string, string> parameters)
        {
            var result = new Sms
            {
                Message = ApplyParameters(MessageTemplate, parameters),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                FromPhoneNumber = FromPhoneNumber
            };

            return result;
        }

        private string ApplyParameters(string template, IDictionary<string, string> parameters)
        {
            if (parameters == null) return template;

            foreach (var parameter in parameters)
            {
                template.Replace($"{{{parameter.Key}}}", parameter.Value);
            }

            return template;
        }
    }
}