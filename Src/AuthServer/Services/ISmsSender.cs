using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;

namespace AuthGuard.Services
{
    public interface ISmsSender
    {
        Task<Sms> SendSmsAsync(string toPhoneNumber, Template template, IDictionary<string, string> parameters);
    }
}