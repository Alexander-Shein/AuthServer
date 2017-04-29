using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthGuard.Services.Security
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
        }

        public async Task<SecurityCode> Get(int code)
        {
            return await context.Set<SecurityCode>().FirstOrDefaultAsync(x => x.Code == code);
        }

        public void Delete(SecurityCode securityCode)
        {
            context.Set<SecurityCode>().Remove(securityCode);
        }
    }
}