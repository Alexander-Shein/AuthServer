using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthGuard.Api;
using DddCore.Contracts.BLL.Errors;

namespace AuthGuard.Services.Passwordless
{
    public interface IPasswordlessService
    {
        Task<OperationResult> SendLogInLinkAsync(UserNameIm im);
        Task<OperationResult> SendSignUpLinkAsync(UserNameIm im);
    }

    public class PasswordlessService : IPasswordlessService
    {
        public Task<OperationResult> SendLogInLinkAsync(UserNameIm im)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> SendSignUpLinkAsync(UserNameIm im)
        {
            throw new NotImplementedException();
        }
    }
}