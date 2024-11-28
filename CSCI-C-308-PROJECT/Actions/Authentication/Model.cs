namespace CSCI_308_TEAM5.API.Actions.Authentication
{
    public sealed record RiderArgs
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public AddressArgs Address { get; set; }
    }

    public record UsersArgs
    {
        public string FullName { get; set; }

        public string EmailAddress { get; set; }
    }

    public record TokenInfo(Guid RefreshToken, string Token, int Expires)
    {
        public string TokenType => "Bearer";
    }

    public record ProfileInfo
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
    }
}
