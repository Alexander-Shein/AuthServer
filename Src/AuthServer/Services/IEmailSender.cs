using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string templateName, IDictionary<string, string> parameters);
    }
}
