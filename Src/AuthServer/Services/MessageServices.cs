using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.Data;
using AuthGuard.Data.Entities;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace AuthGuard.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly ILogger<AuthMessageSender> _logger;
        private readonly ApplicationDbContext context;

        public AuthMessageSender(ILogger<AuthMessageSender> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public async Task SendEmailAsync(string toEmail, string templateName, IDictionary<string, string> parameters)
        {
            if (String.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentNullException(nameof(templateName));
            }

            var emailTemplate =
                await context
                    .Set<EmailTemplate>()
                    .FirstOrDefaultAsync(x => x.Template.Name == templateName);

            if (emailTemplate == null)
            {
                throw new ArgumentException($"Email template '{emailTemplate}' does not exists.");
            }

            var email = emailTemplate.Render(parameters);
            email.ToEmail = toEmail;
            await SendEmailAsync(email);

            email.IsSent = true;

            context.Add(email);
            await context.SaveChangesAsync();
        }

        public async Task SendSmsAsync(string toPhoneNumber, string templateName, IDictionary<string, string> parameters)
        {
            if (String.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentNullException(nameof(templateName));
            }

            var smsTemplate =
                await context
                    .Set<SmsTemplate>()
                    .FirstOrDefaultAsync(x => x.Template.Name == templateName);

            var sms = smsTemplate.Render(parameters);
            sms.ToPhoneNumber = toPhoneNumber;

            context.Add(sms);
            await context.SaveChangesAsync();
        }

        #region Private Methods

        private async Task SendEmailAsync(Email email)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(email.FromName, email.FromEmail));
            mimeMessage.To.Add(new MailboxAddress(String.Empty, email.ToEmail));
            mimeMessage.Subject = email.Subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = email.Body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("aliaksandr.shein@gmail.com", "shayne1montik2");
                await client.SendAsync(mimeMessage);
                client.Disconnect(true);
            }
        }

        #endregion
    }
}