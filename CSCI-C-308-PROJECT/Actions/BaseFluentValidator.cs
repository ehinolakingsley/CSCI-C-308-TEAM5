using CSCI_308_TEAM5.API.Extensions;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace CSCI_308_TEAM5.API.Actions
{
    public abstract class BaseFluentValidator<TArgs> : AbstractValidator<TArgs>
    {
        protected BaseFluentValidator()
        {
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        }

        protected override bool PreValidate(ValidationContext<TArgs> context, ValidationResult result)
        {
            if (context.InstanceToValidate is null)
            {
                result.Errors.Add(new ValidationFailure(nameof(TArgs), "Request data cannot be null"));
                return false;
            }
            return true;
        }
    }

}
