using System.Threading.Tasks;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;

namespace AuthGuard.Services.Apps
{
    public class ClientsStore : IClientStore
    {
        readonly ApplicationDbContext context;

        readonly string[] allowedScopes =
        {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            IdentityServerConstants.StandardScopes.Email
        };

        public ClientsStore(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var app = await context.Set<App>().FirstOrDefaultAsync(x => x.Key == clientId);

            if (app == null) return null;

            var client = new Client
            {
                ClientUri = app.WebsiteUrl,
                AllowedScopes = allowedScopes,
                ClientId = clientId,
                AllowOfflineAccess = true,
                AccessTokenType = AccessTokenType.Jwt,
                ClientName = app.DisplayName,
                EnableLocalLogin = app.IsLocalAccountEnabled,
                Enabled = app.IsActive,
                RequireClientSecret = false
            };
            return client;
        }
    }
}