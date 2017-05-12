using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;

namespace AuthGuard.SL.Security
{
    public interface ISecurityCodesService
    {
        void Insert(SecurityCode securityCode);
        Task<SecurityCode> Get(int code);
        void Delete(SecurityCode securityCode);
    }
}