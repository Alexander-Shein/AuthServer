using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthGuard.SL.Security
{
    public class SecurityCodesService : ISecurityCodesService
    {
        readonly ApplicationDbContext context;

        public SecurityCodesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Insert(SecurityCode securityCode)
        {
            context.Set<SecurityCode>().Add(securityCode);

            foreach (var securityCodeParameter in securityCode.Parameters)
            {
                context.Set<SecurityCodeParameter>().Add(securityCodeParameter);
            }
        }

        public async Task<SecurityCode> Get(int code)
        {
            return await
                context
                .Set<SecurityCode>()
                .Include(x => x.Parameters)
                .FirstOrDefaultAsync(x => x.Code == code);
        }

        public void Delete(SecurityCode securityCode)
        {
            if (securityCode == null) return;
            foreach (var securityCodeParameter in securityCode.Parameters)
            {
                context.Set<SecurityCodeParameter>().Remove(securityCodeParameter);
            }

            context.Set<SecurityCode>().Remove(securityCode);
        }
    }
}