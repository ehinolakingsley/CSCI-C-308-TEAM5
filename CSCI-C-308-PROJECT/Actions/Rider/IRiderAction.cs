namespace CSCI_308_TEAM5.API.Actions.Rider
{
    public interface IRiderAction
    {
        Task<IActionResult> requestForRider(RideRequestArgs args);

        Task<IActionResult> riderHistory();

        Task<IActionResult> riderAddresses();

        Task<IActionResult> addAddress(AddressArgs args);

        Task<IActionResult> cancelRideRequest(Guid requestId);

        Task<IActionResult> getDestinationAddress();

        Task<IActionResult> updateDefaultAddress(Guid addressId);

        Task<IActionResult> removeAddress(Guid addressId);
    }
}
