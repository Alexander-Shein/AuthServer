using System.Linq;
using AuthGuard.BLL.Domain.Entities.Common;
using AuthGuard.BLL.Domain.Entities.Identity.BusinessRules;
using DddCore.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.State;
using FluentValidation;

namespace AuthGuard.BLL.Domain.Entities.Identity
{
    public class IdentityResourceBusinessRules : BusinessRulesValidatorBase<IdentityResource>
    {
        public IdentityResourceBusinessRules(
            IIdentityResourceUniqueNameValidator identityResourceUniqueName,
            IIdentityClaimsExistValidator identityClaimsExistValidator)
        {
            RuleFor(x => x.CrudState)
                .Equal(CrudState.Unchanged)
                    .When(x => x.IsReadOnly)
                    .WithMessage("Cannot modify readonly identity resource.")
                    .WithErrorCode(AuthErrorCode.BusinessRule);

            RuleFor(x => x.Name)
                .NotNull()
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .NotEmpty()
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .Length(2, 200)
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .Matches(@"^[a-z_]+$")
                    .WithMessage("'{PropertyName}' must be letters 'a-z' and '_'.")
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .MustAsync(identityResourceUniqueName.IsUniqueAsync)
                    .When(x => x.CrudState == CrudState.Added || x.IsNameChanged)
                    .WithMessage(x => $"Identity resource with name '{x.Name}' already exists.")
                    .WithErrorCode(AuthErrorCode.BusinessRule);

            RuleFor(x => x.DisplayName)
                .NotNull()
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .NotEmpty()
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .Length(2, 200)
                    .WithErrorCode(AuthErrorCode.BusinessRule);

            RuleFor(x => x.Description)
                .NotNull()
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .Length(0, 1000)
                    .WithErrorCode(AuthErrorCode.BusinessRule);

            RuleFor(x => x.Claims)
                .NotNull()
                    .WithMessage("Identity resource must have at least 1 user claim.")
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .NotEmpty()
                    .WithMessage("Identity resource must have at least 1 user claim.")
                    .WithErrorCode(AuthErrorCode.BusinessRule)
                .MustAsync((x, token) => identityClaimsExistValidator.AreIdentityClaimsExist(x.Select(c => c.IdentityClaimId), token))
                    .When(x => x.CrudState != CrudState.Deleted)
                    .WithMessage("Identity claims are not exist.")
                    .WithErrorCode(AuthErrorCode.BusinessRule);
        }
    }
}