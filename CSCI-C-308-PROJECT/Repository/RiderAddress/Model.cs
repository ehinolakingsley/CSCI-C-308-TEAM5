namespace CSCI_308_TEAM5.API.Repository.RiderAddress
{
    sealed record RiderAddressTbModel : RiderAddressTbArgs
    {
        public Guid UserId { get; set; }

        public Guid AddressId { get; set; }

        public DateTime DateCreated { get; set; }
    }

    record RiderAddressTbArgs
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }

}
