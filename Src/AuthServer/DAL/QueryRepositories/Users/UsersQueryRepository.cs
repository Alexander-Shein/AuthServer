using System;
using System.Threading.Tasks;
using DddCore.Contracts.DAL;
using DddCore.DAL.QueryStack.Dapper;
using Microsoft.Extensions.Options;

namespace AuthGuard.DAL.QueryRepositories.Users
{
    public class UsersQueryRepository : QueryRepositoryBase, IUsersQueryRepository
    {
        public UsersQueryRepository(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<bool> IsUserWithEmailExist(string email)
        {
            var sql = "SELECT TOP 1 [Id] FROM [AspNetUsers] WHERE [Email] = @Email;";

            var result = await GetAsync<string>(sql, new {Email = email});
            return !String.IsNullOrEmpty(result);
        }

        public async Task<bool> IsUserWithPhoneExist(string phone)
        {
            var sql = "SELECT TOP 1 [Id] FROM [AspNetUsers] WHERE [PhoneNumber] = @Phone;";

            var result = await GetAsync<string>(sql, new { Phone = phone });
            return !String.IsNullOrEmpty(result);
        }
    }
}