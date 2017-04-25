using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace AuthGuard.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly ApplicationDbContext context;

        public AuthMessageSender(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Email> SendEmailAsync(string toEmail, Template template, IDictionary<string, string> parameters)
        {
            var emailTemplate =
                await context
                    .Set<EmailTemplate>()
                    .FirstOrDefaultAsync(x => x.Template == template && x.IsActive);

            if (emailTemplate == null)
            {
                throw new ArgumentException($"Email template '{template}' does not exist or is not active.");
            }

            var email = emailTemplate.Render(toEmail, parameters);
            await SendEmailAsync(email);

            context.Add(email);
            return email;
        }

        public async Task<Sms> SendSmsAsync(string toPhoneNumber, Template template, IDictionary<string, string> parameters)
        {
            var smsTemplate =
                await context
                    .Set<SmsTemplate>()
                    .FirstOrDefaultAsync(x => x.Template == template && x.IsActive);

            if (smsTemplate == null)
            {
                throw new ArgumentException($"Sms template '{template}' does not exist or is not active.");
            }

            var sms = smsTemplate.Render(toPhoneNumber, parameters);

            context.Add(sms);
            return sms;
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
            email.IsSent = true;
        }

        #endregion
    }
}