using System;
using System.Collections.Generic;

namespace AuthGuard.BLL.Domain.Entities
{
    public class App
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string WebsiteUrl { get; set; }
        public bool IsLocalAccountEnabled { get; set; }
        public bool IsRememberLogInEnabled { get; set; }
        public bool IsSecurityQuestionsEnabled { get; set; }
        public bool IsActive { get; set; }
        public int UsersCount { get; set; }
        public LocalAccountSettings EmailSettings { get; set; }
        public LocalAccountSettings PhoneSettings { get; set; }
        public ICollection<ExternalProvider> ExternalProviders { get; set; }
    }
}