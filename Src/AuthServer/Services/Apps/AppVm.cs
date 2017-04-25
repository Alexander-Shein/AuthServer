using System;
using System.Collections.Generic;

namespace AuthGuard.Services.Apps
{
    public class AppVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public bool IsLocalAccountEnabled { get; set; }
        public bool IsRememberLogInEnabled { get; set; }
        public bool IsSecurityQuestionsEnabled { get; set; }
        public LocalAccountSettingsVm EmailSettings { get; set; }
        public LocalAccountSettingsVm PhoneSettings { get; set; }
        public IEnumerable<ExternalProviderVm> ExternalProviders { get; set; }
    }
}