using System;
using System.Collections.Generic;

namespace AuthGuard.Data.Entities
{
    public class EmailTemplate
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }

        public string FromNameTemplate { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }

        public string FromEmail { get; set; }
        public EmailBodyFormat EmailBodyFormat { get; set; }
        public bool IsActive { get; set; }

        public Template Template { get; set; }
        public ICollection<EmailTemplateAttachment> EmailTemplateAttachments { get; set; }

        public Email Render(IDictionary<string, string> parameters)
        {
            var result = new Email
            {
                Subject = ApplyParameters(SubjectTemplate, parameters),
                Body = ApplyParameters(BodyTemplate, parameters),
                FromName = ApplyParameters(FromNameTemplate, parameters),
                FromEmail = FromEmail,
                Id = Guid.NewGuid()
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