using System;

namespace AuthServer.Services.Users.Models.Input
{
    public class ConfirmAccountIm
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public string Provider { get; set; }
    }
}
