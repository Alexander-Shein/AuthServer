using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.DAL.DomainStack.EntityFramework;
using DddCore.DAL.DomainStack.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthGuard.DAL.Repositories.Security
{
    public class SecurityCodesRepository : Repository<SecurityCode, Guid>, ISecurityCodesRepository
    {
        public SecurityCodesRepository(IDataContext dataContext, IUserContext<Guid> userContext) : base(dataContext, userContext)
        {
        }

        public async Task<SecurityCode> ReadByCodeAsync(int code)
        {
            return await
                EntityFrameworkQueryableExtensions.Include<SecurityCode, ICollection<SecurityCodeParameter>>(GetDbSet(), x => x.Parameters)
                    .FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}