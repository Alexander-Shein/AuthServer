using System;

namespace AuthServer.Data.Entities
{
    public class SmsTemplate
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public string BodyTemplate { get; set; }
        public bool IsActive { get; set; }

        public Template Template { get; set; }
    }
}