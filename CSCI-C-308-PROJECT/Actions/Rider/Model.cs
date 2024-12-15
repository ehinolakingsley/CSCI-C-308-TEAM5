namespace CSCI_308_TEAM5.API.Actions.Rider
{
    public sealed record RiderAddressInfo
    {
        public bool DefaultAddress { get; set; }

        public string Address => AddressInfo?.ToString() ?? "N/A";

        public AddressArgs AddressInfo { get; set; }
    }

    public sealed record RideRequestArgs
    {
        public DateTime PickUpDateTime { get; set; }

        public Guid PickUpAddressID { get; set; }

        public string AdditionalInfomation { get; set; }
    }

    public sealed record RideHistoryInfo
    {
        public Guid RequestID { get; set; }

        public string Status { get; set; }

        public string PickUpDateTime { get; set; }

        public string PickUpLocation { get; set; }
    }
}
