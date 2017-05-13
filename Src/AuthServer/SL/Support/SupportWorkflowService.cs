using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.SL.Notifications;
using DddCore.Contracts.BLL.Errors;

namespace AuthGuard.SL.Support
{
    public class SupportWorkflowService : ISupportWorkflowService
    {
        readonly ApplicationDbContext context;
        readonly IEmailSender emailSender;
        const string CustomerMessagesEmail = "alex-ololo@mailinator.com";

        public SupportWorkflowService(ApplicationDbContext context, IEmailSender emailSender)
        {
            this.context = context;
            this.emailSender = emailSender;
        }

        public async Task<(MessageVm Message, OperationResult OperationResult)> SendMessage(MessageIm im)
        {
            var parameters = new Dictionary<string, string>
            {
                {"FirstName", im.FirstName},
                {"LastName", im.LastName},
                {"CompanyName", im.CompanyName},
                {"Subject", im.Subject},
                {"Body", im.Message}
            };

            var email = await emailSender.SendEmailAsync(CustomerMessagesEmail, Template.CustomerMessage, parameters);
            email.FromEmail = im.FromEmail;

            await context.SaveChangesAsync();

            var result = new MessageVm
            {
                FromEmail = im.FromEmail,
                CompanyName = im.CompanyName,
                Subject = im.Subject,
                Message = im.Message,
                FirstName = im.FirstName,
                Id = email.Id,
                LastName = im.LastName
            };

            return (result, new OperationResult());
        }
    }
}