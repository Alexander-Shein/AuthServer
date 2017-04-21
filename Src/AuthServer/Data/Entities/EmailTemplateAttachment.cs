using System;

namespace AuthGuard.Data.Entities
{
    public class EmailTemplateAttachment
    {
        public Guid Id { get; set; }
        public Guid EmailTemplateId { get; set; }
        public string FileName { get; set; }
    }
}