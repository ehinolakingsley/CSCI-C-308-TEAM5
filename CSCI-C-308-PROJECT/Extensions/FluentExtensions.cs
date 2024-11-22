using FluentValidation;
using FluentValidation.Validators;

namespace CSCI_308_TEAM5.API.Extensions
{
    public static class FluentExtensions
    {
        public static IRuleBuilderOptions<T, AddressArgs> IsAddress<T>(this IRuleBuilder<T, AddressArgs> ruleBuilder) =>
            ruleBuilder.SetValidator(new IsAddressValidator<T>());

        public class IsAddressValidator<TProperty> : PropertyValidator<TProperty, AddressArgs>
        {
            public override string Name => "IsAddressValidator";

            string errMsg = "'{PropertyName}' is required.";

            public override bool IsValid(ValidationContext<TProperty> context, AddressArgs value)
            {
                if (value is null)
                    return false;

                var response = value.addressValid(out string expMsg);

                errMsg = $"'{{PropertyName}}' {expMsg.ToLower()}.";
                return response;
            }

            protected override string GetDefaultMessageTemplate(string errorCode)
            => errMsg;
        }
    }
}
