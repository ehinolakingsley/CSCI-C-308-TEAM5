using CSCI_308_TEAM5.API.Services.Config;

namespace CSCI_308_TEAM5.API.Repository.RiderRequest
{
    interface IRiderRequestTb
    {
        Task<IEnumerable<RiderRequestTbModel>> get(Guid riderID, DateOnly rideDate);

        Task<IEnumerable<RiderRequestTbModel>> getAll(Guid riderID);

        Task<Guid> addRideRequest(RiderRequestTbArgs args);

        Task<RiderRequestTbModel> getRequest(Guid requestID);

        Task updateStatus(Guid requestID, RiderRequestStatus status);
    }


    sealed class RiderRequestTb(IConfigService configService) : IRiderRequestTb
    {
    }
}
