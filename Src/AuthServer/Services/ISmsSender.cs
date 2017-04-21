using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthGuard.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string phoneNumber, string templateName, IDictionary<string, string> parameters);
    }
}
