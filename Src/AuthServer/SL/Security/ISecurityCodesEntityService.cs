using AuthGuard.BLL.Domain.Entities;

namespace AuthGuard.SL.Security
{
    public interface ISecurityCodesEntityService
    {
        void Insert(SecurityCode securityCode);
        void Delete(SecurityCode securityCode);
    }
}