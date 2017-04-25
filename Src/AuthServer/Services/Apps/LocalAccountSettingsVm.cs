namespace AuthGuard.Services.Apps
{
    public class LocalAccountSettingsVm
    {
        public bool IsEnabled { get; set; }
        public bool IsPasswordlessEnabled { get; set; }
        public bool IsPasswordEnabled { get; set; }
        public bool IsConfirmationRequired { get; set; }
        public bool IsSearchRelatedProviderEnabled { get; set; }
    }
}