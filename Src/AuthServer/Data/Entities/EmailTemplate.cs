using System;
using System.Collections.Generic;

namespace AuthServer.Data.Entities
{
    public class EmailTemplate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BodyTemplate { get; set; }
        public string SubjectTemplate { get; set; }
        public EmailBodyFormat EmailBodyFormat { get; set; }

        public ICollection<EmailTemplateAttachment> EmailTemplateAttachments { get; set; }

        public Email Render(IDictionary<string, string> parameters)
        {
            var result = new Email
            {
                Subject = ApplyParameters(SubjectTemplate, parameters),
                Message = ApplyParameters(BodyTemplate, parameters)
            };

            return result;
        }

        private string ApplyParameters(string template, IDictionary<string, string> parameters)
        {
            if (parameters == null) return template;

            foreach (var parameter in parameters)
            {
                template.Replace($"{parameter.Key}", parameter.Value);
            }

            return template;
        }
    }
}