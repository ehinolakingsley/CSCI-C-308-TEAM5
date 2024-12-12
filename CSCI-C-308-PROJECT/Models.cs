namespace CSCI_308_TEAM5.API
{
    public enum DeploymentEnv
    {
        Production = 0, // Default
        Staging = 1,
        Development = 2
    }

    public sealed record AddressArgs
    {
        public AddressArgs() { }

        public AddressArgs(string street, string city, string state, string zipCode, string country)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public override string ToString()
        {
            if (!this.addressValid(out _))
                return "N/A";

            if (ZipCode.empty())
                return $"{Street}\n{City}, {State}\n{Country}.";

            return $"{Street}\n{City}, {State} {ZipCode}\n{Country}.";
        }
    }

    public sealed record EmailClientCredentialInfo(string SMTPAddress, int SMTPPort, string SMTPPwd, string SMTPServer);

}
