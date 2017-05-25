using DddCore.BLL.Domain.Entities.GuidEntities;

namespace AuthGuard.BLL.Domain.Entities
{
    public class ExternalProvider : GuidAggregateRootBase
    {
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}