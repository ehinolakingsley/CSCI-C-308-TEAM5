namespace CSCI_308_TEAM5.API.Actions.Authentication
{
    public sealed record RiderArgs
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public AddressArgs Address { get; set; }
    }

    public sealed record UserArgs
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public Security.Roles Role { get; set; }
    }
}
