using AuthGuard.BLL.Domain.Entities.Common;
using DddCore.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.State;
using FluentValidation;

namespace AuthGuard.BLL.Domain.Entities.Identity
{
    public class IdentityClaimBusinessRules : BusinessRulesValidatorBase<IdentityClaim>
    {
        public IdentityClaimBusinessRules()
        {
            RuleFor(x => x.CrudState)
                .Equal(CrudState.Unchanged)
                    .When(x => x.IsReadOnly)
                    .WithMessage("Cannot modify readonly identity claim.")
                    .WithErrorCode(AuthErrorCode.BusinessRule);

            RuleFor(x => x.Type)
                .NotNull()
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .NotEmpty()
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .Length(2, 200)
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .Matches(@"^[a-z_]+$")
                    .WithMessage("Allowed letters and '_' only.")
                    .WithErrorCode(AuthErrorCode.BusinessRule);

            RuleFor(x => x.Description)
                .NotNull()
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .Length(0, 1000)
                    .WithErrorCode(AuthErrorCode.BusinessRule);
        }
    }
}