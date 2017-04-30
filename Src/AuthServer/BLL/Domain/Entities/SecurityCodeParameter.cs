using System;
using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities
{
    public class SecurityCodeParameter : GuidEntityBase
    {
        // EF workaround
        public virtual string NameString
        {
            get => Name.ToString();
            set
            {
                if (Enum.TryParse(value, out SecurityCodeParameterName newValue))
                {
                    Name = newValue;
                }
            }
        }

        public Guid SecurityCodeId { get; set; }
        public SecurityCodeParameterName Name { get; set; }
        public string Value { get; set; }
    }
}