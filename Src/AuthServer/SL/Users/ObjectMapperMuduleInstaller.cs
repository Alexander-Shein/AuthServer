using System;
using AuthGuard.BLL.Domain.Entities;
using AuthGuard.SL.Users.Models.View;
using DddCore.Contracts.Crosscutting.ObjectMapper;
using DddCore.Contracts.Crosscutting.ObjectMapper.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthGuard.SL.Users
{
    public class IdentityObjectMapperModuleInstaller : IObjectMapperModuleInstaller
    {
        public void Install(IObjectMapperConfig config)
        {
            config.Bind<ApplicationUser, UserVm>(c =>
            {
                c.Bind(x => new Guid(x.Id), x => x.Id);
                c.Bind(x => x.Email, x => x.Email);
                c.Bind(x => x.EmailConfirmed, x => x.IsEmailConfirmed);
                c.Bind(x => x.PhoneNumber, x => x.PhoneNumber);
                c.Bind(x => x.PhoneNumberConfirmed, x => x.IsPhoneNumberConfirmed);
                c.Bind(x => x.TwoFactorEnabled, x => x.IsTwoFactorEnabled);
                c.Bind(x => !String.IsNullOrEmpty(x.PasswordHash), x => x.HasPassword);
                c.Bind(x => x.Logins, x => x.ExternalProviders);
            });

            config.Bind<IdentityUserLogin<string>, UserExternalProviderVm>(c =>
            {
                c.Bind(x => x.LoginProvider, x => x.AuthenticationScheme);
                c.Bind(x => x.ProviderDisplayName, x => x.DisplayName);
                c.Bind(x => x.ProviderKey, x => x.Key);
            });
        }
    }
}