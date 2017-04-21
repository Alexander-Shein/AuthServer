using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using IdentityServerWithAspNetIdentity.Data;
using AuthServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerWithAspNetIdentity.Services
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

        public async Task SendEmailAsync(string toEmail, string emailTemplateName, IDictionary<string, string> parameters)
        {
            if (String.IsNullOrWhiteSpace(emailTemplateName))
            {
                throw new ArgumentNullException(nameof(emailTemplateName));
            }

            var emailTemplate =
                await context
                    .Set<EmailTemplate>()
                    .FirstOrDefaultAsync(x => x.Template.Name == emailTemplateName);

            if (emailTemplate == null)
            {
                throw new ArgumentException($"Email template '{emailTemplate}' does not exists.");
            }

            var email = emailTemplate.Render(parameters);
            email.ToEmail = toEmail;
            await SendEmailAsync(email);
        }

        public Task SendSmsAsync(string phoneNumber, string templateName, IDictionary<string, string> parameters)
        {
            return Task.FromResult(0);
        }

        #region Private Methods

        private Task SendEmailAsync(Email email)
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
                client.Send(mimeMessage);
                client.Disconnect(true);
            }

            return Task.FromResult(0);
        }

        #endregion
    }
}