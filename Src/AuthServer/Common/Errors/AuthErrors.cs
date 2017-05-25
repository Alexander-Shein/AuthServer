using DddCore.Contracts.BLL.Errors;

namespace AuthGuard.Common.Errors
{
    public static class AuthErrors
    {
        public static Error NullInputModel = new Error(1, "Model must not be empty.");
    }
}
