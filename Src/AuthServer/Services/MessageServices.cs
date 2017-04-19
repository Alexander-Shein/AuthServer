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
                    .FirstOrDefaultAsync(x => x.Name == emailTemplateName);

            if (emailTemplate == null)
            {
                throw new ArgumentException($"Email template '{emailTemplate}' does not exists.");
            }

            var email = emailTemplate.Render(parameters);
            await SendEmailAsync(toEmail, email.Subject, email.Message);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            _logger.LogInformation("SMS: {number}, Message: {message}", number, message);
            return Task.FromResult(0);
        }

        #region Private Methods

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("AuthGuardian", "aliaksandr.shein@gmail.com"));
            mimeMessage.To.Add(new MailboxAddress("AuthGuardian", email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("aliaksandr.shein@gmail.com", "shayne1montik2");
                client.Send(mimeMessage);
                client.Disconnect(true);
            }

            _logger.LogInformation("Email: {email}, Subject: {subject}, Message: {message}", email, subject, message);
            return Task.FromResult(0);
        }

        #endregion
    }
}
