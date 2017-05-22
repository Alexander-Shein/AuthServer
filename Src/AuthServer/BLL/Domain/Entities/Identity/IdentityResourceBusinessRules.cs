using DddCore.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using FluentValidation;

namespace AuthGuard.BLL.Domain.Entities.Identity
{
    public class IdentityResourceBusinessRules : BusinessRulesValidatorBase<IdentityResource>
    {
        public IdentityResourceBusinessRules()
        {
            RuleFor(x => x.CrudState)
                .Equal(CrudState.Unchanged)
                .When(x => x.IsReadOnly)
                .WithMessage("Cannot modify readonly identity resource.");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Length(2, 200)
                .Matches(@"^[a-z_]+$")
                .WithMessage("Allowed letters and '_' only.");

            RuleFor(x => x.DisplayName)
                .NotNull()
                .NotEmpty()
                .Length(2, 200);

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .Length(2, 1000);

            RuleFor(x => x.Claims)
                .NotNull()
                .NotEmpty();
        }
    }
}