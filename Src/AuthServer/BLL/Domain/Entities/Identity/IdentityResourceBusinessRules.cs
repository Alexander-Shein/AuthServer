using System.Linq;
using AuthGuard.BLL.Domain.Entities.Identity.BusinessRules;
using DddCore.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.State;
using FluentValidation;

namespace AuthGuard.BLL.Domain.Entities.Identity
{
    public class IdentityResourceBusinessRules : BusinessRulesValidatorBase<IdentityResource>
    {
        public IdentityResourceBusinessRules(IIdentityResourceUniqueName identityResourceUniqueName)
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
                    .WithMessage("Allowed letters and '_' only.")
                .MustAsync(identityResourceUniqueName.IsUniqueAsync)
                    .WithMessage(x => $"Identity resource with name '{x.Name}' already exists.");

            RuleFor(x => x.DisplayName)
                .NotNull()
                .NotEmpty()
                .Length(2, 200);

            RuleFor(x => x.Description)
                .NotNull()
                .Length(0, 1000);

            RuleFor(x => x.Claims)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Claims)
                .NotNull()
                    .WithMessage("Identity resource must have at least 1 user claim.")
                .Must(x => x.Any())
                    .WithMessage("Identity resource must have at least 1 user claim.");
        }
    }
}