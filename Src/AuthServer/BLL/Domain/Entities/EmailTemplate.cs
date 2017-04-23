using System;
using System.Collections.Generic;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities
{
    public class EmailTemplate : GuidAggregateRootEntityBase
    {
        public string FromNameTemplate { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }

        public string FromEmail { get; set; }
        public EmailBodyFormat EmailBodyFormat { get; set; }
        public bool IsActive { get; set; }
        public Template Template { get; set; }

        public Email Render(IDictionary<string, string> parameters)
        {
            var result = new Email
            {
                Subject = ApplyParameters(SubjectTemplate, parameters),
                Body = ApplyParameters(BodyTemplate, parameters),
                FromName = ApplyParameters(FromNameTemplate, parameters),
                FromEmail = FromEmail,
                EmailTemplate = this,
                CreatedAt = DateTime.UtcNow
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