using System;

namespace AuthServer.Data.Entities
{
    public class Email
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}