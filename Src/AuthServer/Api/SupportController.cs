using System;
using AuthGuard.Data;
using AuthGuard.Data.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuard.Api
{
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class SupportController : Controller
    {
        public readonly ApplicationDbContext context;

        public SupportController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("messages")]
        public MessageVm Post([FromBody] MessageIm im)
        {
            var domain = new Email
            {
                Id = Guid.NewGuid(),
                FromName = $"{im.FirstName} {im.LastName} ({im.CompanyName})",
                FromEmail = im.FromEmail,
                ToEmail = "",
                Subject = im.Subject,
                Body = im.Message,
                CreatedAt = DateTime.Now
            };

            context.Add(domain);
            context.SaveChanges();

            return new MessageVm
            {
                Id = domain.Id,
                FirstName = im.FirstName,
                LastName = im.LastName,
                CompanyName = im.CompanyName,
                FromEmail = im.FromEmail,
                Subject = im.Subject,
                Message = im.Message
            };
        }
    }

    public class MessageIm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    public class MessageVm : MessageIm
    {
        public Guid Id { get; set; }
    }

}
