using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Errors;
using DddCore.DAL.DomainStack.EntityFramework.Context;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AuthGuard.SL.Notifications
{
    public class NotificationsService : IEmailSender, ISmsSender, INotificationsService
    {
        const string AccountSid = "ACeaba8d4f6486f20a46db5db7c2fd3ddf";
        const string AuthToken = "61733dfbf99ac90b6add0cefa916602c";

        readonly IDataContext context;

        public NotificationsService(IDataContext context)
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

            context.Set<Email>().Add(email);
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

            TwilioClient.Init(AccountSid, AuthToken);

            var message = await MessageResource.CreateAsync(
                to: new PhoneNumber(toPhoneNumber),
                @from: new PhoneNumber(sms.FromPhoneNumber),
                body: sms.Message);

            if (message.ErrorCode.HasValue)
            {
                throw new InvalidOperationException(message.ErrorMessage);
            }

            sms.IsSent = true;

            context.Set<Sms>().Add(sms);
            return sms;
        }

        public async Task<OperationResult> SendMessageAsync(string userName, Template template, IDictionary<string, string> parameters)
        {
            if (ApplicationUser.IsEmail(userName))
            {
                await SendEmailAsync(userName, template, parameters);
            }
            else
            {
                var phone = ApplicationUser.CleanPhoneNumber(userName);

                if (!ApplicationUser.IsPhoneNumber(phone))
                {
                    return OperationResult.Failed(1, "Invalid userName.");
                }

                await SendSmsAsync(phone, template, parameters);
            }

            return OperationResult.Succeed;
        }

        #region Private Methods

        private async Task SendEmailAsync(Email email)
        {
            //9a20e2a97a07670b75e787047a63508887a63b3d

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