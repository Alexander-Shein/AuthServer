using System;

namespace AuthServer.Services.Users.Models.Input
{
    public class ConfirmAccountIm : ConfirmationCodeIm
    {
        public Guid UserId { get; set; }
        public string Provider { get; set; }
    }
}
