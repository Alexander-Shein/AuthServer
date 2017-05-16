using DddCore.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using FluentValidation;

namespace AuthGuard.BLL.Domain.Entities.Identity
{
    public class IdentityClaimBusinessRules : BusinessRulesValidatorBase<IdentityClaim>
    {
        public IdentityClaimBusinessRules()
        {
            RuleFor(x => x.CrudState).Equal(CrudState.Unchanged)
                .When(x => x.IsReadOnly)
                .WithMessage("Cannot modify readonly identity claim.");

            RuleFor(x => x.Type).NotNull()
                .NotEmpty()
                .Length(2, 200)
                .Matches(@"^[a-z_]+$")
                .WithMessage("Allowed letters and '_' only.");
        }
    }
}