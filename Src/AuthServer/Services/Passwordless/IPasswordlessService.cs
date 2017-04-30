using System;
using System.Threading.Tasks;
using AuthGuard.Api;
using DddCore.Contracts.BLL.Errors;

namespace AuthGuard.Services.Passwordless
{
    public interface IPasswordlessService
    {
        Task<OperationResult> SendLogInLinkAsync(CallbackUrlAndUserNameIm im);
        Task<OperationResult> SendSignUpLinkAsync(CallbackUrlAndUserNameIm im);

        Task<OperationResult> SignUpAsync(CodeAndUserNameIm im);
        Task<OperationResult> LogInAsync(CodeIm im);
    }

    public class CallbackUrlAndUserNameIm : UserNameIm
    {
        public string CallbackUrl { get; set; }
    }

    public class CodeAndUserNameIm : UserNameIm
    {
        public int Code { get; set; }
    }

    public class CodeIm
    {
        public int Code { get; set; }
    }
}