using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthGuard.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string templateName, IDictionary<string, string> parameters);
    }
}
