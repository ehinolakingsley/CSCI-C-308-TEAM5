namespace CSCI_308_TEAM5.API.Actions.Rider
{
    public sealed record RiderAddressInfo
    {
        public bool DefaultAddress { get; set; }

        public string Address => AddressInfo?.ToString() ?? "N/A";

        public AddressArgs AddressInfo { get; set; }
    }
}
