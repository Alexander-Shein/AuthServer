using DddCore.BLL.Domain.Entities.BusinessRules;
using FluentValidation;

namespace AuthGuard.BLL.Domain.Entities.ApiResources
{
    public class ApiResourceBusinessRules : BusinessRulesValidatorBase<ApiResource>
    {
        public ApiResourceBusinessRules()
        {
            RuleFor(x => x.Name).NotNull()
                .NotEmpty()
                .Length(2, 200)
                .Matches(@"^[a-z_-.]+$")
                .WithMessage("Allowed letters and '_', '-', '.' only.");

            RuleFor(x => x.DisplayName)
                .NotNull()
                .NotEmpty()
                .Length(2, 200);

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .Length(2, 1000);

            RuleFor(x => x.Secret)
                .NotNull()
                .NotEmpty()
                .Length(6, 2000);
        }
    }
}