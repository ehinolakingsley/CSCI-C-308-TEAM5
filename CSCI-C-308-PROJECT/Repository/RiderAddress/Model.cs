namespace CSCI_308_TEAM5.API.Repository.RiderAddress
{
    public sealed record RiderAddressTbModel : RiderAddressTbArgs
    {
        public Guid userID { get; set; }

        public Guid addressID { get; set; }

        public DateTime dateCreated { get; set; }
    }

    public record RiderAddressTbArgs
    {
        public string street { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string country { get; set; }

        public string zipCode { get; set; }
    }

}
