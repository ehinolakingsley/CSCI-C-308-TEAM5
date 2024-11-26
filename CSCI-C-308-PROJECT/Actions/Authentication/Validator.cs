using CSCI_308_TEAM5.API.Extensions;

namespace CSCI_308_TEAM5.API.Actions.Authentication
{
    public sealed class RiderArgsValidator : BaseFluentValidator<RiderArgs>
    {
        public RiderArgsValidator()
        {
            RuleFor(d => d.FullName).NotEmpty().MaximumLength(40);
            RuleFor(d => d.PhoneNumber).MaximumLength(10);
            RuleFor(d => d.EmailAddress).MaximumLength(40);
            RuleFor(d => d.Address).IsAddress();
        }
    }
}
