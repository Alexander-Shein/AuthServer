using FluentValidation;
using FluentValidation.Resources;

namespace AuthGuard.BLL.Domain.Entities.Common
{
    public static class AuthDefaultValidatorOptions
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, AuthErrorCode errorCode)
        {
            return rule.Configure(config =>
            {
                config.CurrentValidator.ErrorCodeSource = new StaticStringSource(errorCode.ToString("D"));
            });
        }
    }
}