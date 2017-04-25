using System;
using System.Collections.Generic;
using DddCore.BLL.Domain.Entities.GuidEntities;
using DddCore.Contracts.BLL.Domain.Entities.Audit.At;

namespace AuthGuard.BLL.Domain.Entities
{
    public class App : GuidAggregateRootEntityBase, ICreatedAt
    {
        public string UserId { get; set; }

        public string DisplayName { get; set; }
        public string Key { get; set; }
        public string WebsiteUrl { get; set; }
        public bool IsLocalAccountEnabled { get; set; }
        public bool IsRememberLogInEnabled { get; set; }
        public bool IsSecurityQuestionsEnabled { get; set; }
        public bool IsActive { get; set; }
        public int UsersCount { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsEmailEnabled { get; set; }
        public bool IsEmailConfirmationRequired { get; set; }
        public bool IsEmailPasswordEnabled { get; set; }
        public bool IsEmailPasswordlessEnabled { get; set; }
        public bool IsEmailSearchRelatedProviderEnabled { get; set; }
        public bool IsPhoneEnabled { get; set; }
        public bool IsPhoneConfirmationRequired { get; set; }
        public bool IsPhonePasswordEnabled { get; set; }
        public bool IsPhonePasswordlessEnabled { get; set; }

        public ICollection<AppExternalProvider> ExternalProviders { get; set; }
    }
}