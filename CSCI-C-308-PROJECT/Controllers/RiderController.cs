using CSCI_308_TEAM5.API.Actions.Authentication;
using CSCI_308_TEAM5.API.Actions.Rider;
using Microsoft.AspNetCore.Authorization;

namespace CSCI_308_TEAM5.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RiderController(IRiderAction riderAction, IAuthenticationAction authenticationAction) : ControllerBase
    {
        /// <summary>
        /// Updates a rider's profile information.
        /// </summary>
        /// <param name="args">The updated profile information.</param>
        /// <returns>A task that returns the result of the operation as a string.</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> updateRiderProfile(RiderArgs args) => await authenticationAction.updateRiderProfile(args);

        /// <summary>
        /// Requests a ride for the authenticated rider with the provided details.
        /// </summary>
        /// <param name="args">The ride request details.</param>
        /// <returns>A task that returns the result of the operation as a string.</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> requestForRider(RideRequestArgs args) => await riderAction.requestForRider(args);

        /// <summary>
        /// Retrieves the ride history for the authenticated rider.
        /// </summary>
        /// <returns>A task that returns the ride history as a collection of <see cref="RideHistoryInfo"/> objects.</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RideHistoryInfo>))]
        public async Task<IActionResult> riderHistory() => await riderAction.riderHistory();

        /// <summary>
        /// Retrieves the list of addresses associated with the authenticated rider.
        /// </summary>
        /// <returns>A task that returns the list of addresses as a collection of <see cref="RiderAddressInfo"/> objects.</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RiderAddressInfo>))]
        public async Task<IActionResult> riderAddresses() => await riderAction.riderAddresses();

        /// <summary>
        /// Adds a new address for the authenticated rider.
        /// </summary>
        /// <param name="args">The details of the new address.</param>
        /// <returns>A task that returns the result of the operation as a string.</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> addAddress(AddressArgs args) => await riderAction.addAddress(args);

        /// <summary>
        /// Cancels a ride request with the specified ID.
        /// </summary>
        /// <param name="requestID">The ID of the ride request to cancel.</param>
        /// <returns>A task that returns the result of the operation as a string.</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> cancelRideRequest(Guid requestID) => await riderAction.cancelRideRequest(requestID);

        /// <summary>
        /// Retrieves the destination address for the current ride request.
        /// </summary>
        /// <returns>A task that returns the destination address as a <see cref="AddressArgs"/> object.</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(AddressArgs))]
        public async Task<IActionResult> getDestinationAddress() => await riderAction.getDestinationAddress();

        /// <summary>
        /// Sets the default address for the authenticated rider to the specified address ID.
        /// </summary>
        /// <param name="addressID">The ID of the address to set as default.</param>
        /// <returns>A task that returns the result of the operation as a string.</returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> setRiderDefaultAddress(Guid addressID) => await riderAction.updateDefaultAddress(addressID);

        /// <summary>
        /// Removes the address associated with the specified ID for the authenticated rider.
        /// </summary>
        /// <param name="addressID">The ID of the address to remove.</param>
        /// <returns>A task that returns the result of the operation as a string.</returns>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> removeAddress(Guid addressID) => await riderAction.removeAddress(addressID);

    }
}
