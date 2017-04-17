using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly ILogger<AuthMessageSender> _logger;

        public AuthMessageSender(ILogger<AuthMessageSender> logger)
        {
            _logger = logger;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("AuthGuardian", "alexa-montana@live.com"));
            mimeMessage.To.Add(new MailboxAddress("AuthGuardian", email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp-pulse.com", 465, true);
                client.Authenticate("alexa-montana@live.com", "F2qKJEpdj9");
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                // Note: since we don't have an OAuth2 token, disable 	// the XOAUTH2 authentication mechanism.     client.Authenticate("anuraj.p@example.com", "password");
                client.Send(mimeMessage);
                client.Disconnect(true);
            }

            _logger.LogInformation("Email: {email}, Subject: {subject}, Message: {message}", email, subject, message);
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            _logger.LogInformation("SMS: {number}, Message: {message}", number, message);
            return Task.FromResult(0);
        }
    }
}
