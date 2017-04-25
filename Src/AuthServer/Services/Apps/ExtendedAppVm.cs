using System;

namespace AuthGuard.Services.Apps
{
    public class ExtendedAppVm : ExtendedAppIm
    {
        public Guid Id { get; set; }
        public int UsersCount { get; set; }
    }
}