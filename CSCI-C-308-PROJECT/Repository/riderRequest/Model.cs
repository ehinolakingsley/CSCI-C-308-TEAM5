using System.ComponentModel;

namespace CSCI_308_TEAM5.API.Repository.RiderRequest
{
    sealed record RiderRequestTbModel : RiderRequestTbArgs
    {
        public Guid requestID { get; set; }

        public Guid? assignedDriverID { get; set; }

        public int requestStatus { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime lastModified { get; set; }
    }

    internal record RiderRequestTbArgs
    {
        public Guid riderID { get; set; }

        public Guid pickUpAddressID { get; set; }

        public DateTime pickUpDateTime { get; set; }
    }


    public enum RiderRequestStatus
    {
        [Description("Rider requested for a ride and awaiting driver's acknowledgment")]
        Pending = 0,

        [Description("Driver acknowledges rider's request")]
        Acknowledged = 1,

        [Description("Driver reject rider's request")]
        Rejected = 2,

        [Description("Rider terminates request for ride")]
        Terminated = 3,

        [Description("Driver has completed the ride request")]
        Completed = 4,
    }
}
