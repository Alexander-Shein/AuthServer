using System;

namespace AuthServer.Data.Entities
{
    public class Email
    {
        public Guid Id { get; set; }
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}