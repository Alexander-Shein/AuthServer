using System;

namespace AuthGuard.BLL.Domain.Entities
{
    public class Sms
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string ToPhoneNumber { get; set; }
        public string FromPhoneNumber { get; set; }
        public bool IsSent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}