using System;
using System.Collections.Generic;
using DddCore.BLL.Domain.Entities.BusinessRules;
using DddCore.BLL.Domain.Entities.GuidEntities;
using DddCore.Contracts.BLL.Domain.Entities.Audit.At;
using FluentValidation;

namespace AuthGuard.BLL.Domain.Entities
{
    public class App : GuidAggregateRootEntityBase, ICreatedAt
    {
        string displayName;

        public string UserId { get; set; }

        public string DisplayName
        {
            get => displayName;
            set => displayName = value?.Trim();
        }

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

        public ICollection<AppExternalProvider> ExternalProviders { get; set; } = new List<AppExternalProvider>();
    }

    public class AppBusinessRules : BusinessRulesValidatorBase<App>
    {
        public AppBusinessRules()
        {
            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .Length(2, 100);

            RuleFor(x => x.WebsiteUrl)
                .NotEmpty()
                .Length(10, 100)
                .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute));
        }
    }
}