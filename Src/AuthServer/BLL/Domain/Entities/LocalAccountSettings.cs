using System.Collections.Generic;
using System.Linq;
using DddCore.BLL.Domain.ValueObjects;

namespace AuthGuard.BLL.Domain.Entities
{
    public class LocalAccountSettings : ValueObjectBase<LocalAccountSettings>
    {
        public bool IsEnabled { get; set; }
        public bool IsPasswordlessEnabled { get; set; }
        public bool IsPasswordEnabled { get; set; }
        public bool IsConfirmationRequired { get; set; }
        public bool IsSearchRelatedProviderEnabled { get; set; }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return Enumerable.Empty<object>();
        }
    }
}