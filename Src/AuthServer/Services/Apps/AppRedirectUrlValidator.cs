using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace AuthGuard.Services.Apps
{
    public class AppRedirectUrlValidator : IRedirectUriValidator
    {
        protected bool StringCollectionContainsString(IEnumerable<string> uris, string requestedUri)
        {
            if (uris.IsNullOrEmpty()) return false;

            return uris.Contains(requestedUri, StringComparer.OrdinalIgnoreCase);
        }

        public virtual Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
        {
            if (String.IsNullOrWhiteSpace(requestedUri)) return Task.FromResult(false);
            return Task.FromResult(String.Equals(GetLeftPart(requestedUri), GetLeftPart(client.ClientUri), StringComparison.OrdinalIgnoreCase));
        }

        public virtual Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {
            return Task.FromResult(StringCollectionContainsString(client.PostLogoutRedirectUris, requestedUri));
        }

        private string GetLeftPart(string url)
        {
            var uri = new Uri(url);
            var result = uri.Scheme + "://" + uri.Host + ":" + uri.Port;
            return result;
        }
    }
}