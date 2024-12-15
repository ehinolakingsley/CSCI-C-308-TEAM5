using CSCI_308_TEAM5.API.Actions.BaseAction;
using CSCI_308_TEAM5.API.Repository.RiderRequest;
using CSCI_308_TEAM5.API.Services.Email;

namespace CSCI_308_TEAM5.API.Actions.Rider
{
    sealed class RiderAction(IAggregateServices services) : BaseTeam5Action(services), IRiderAction
    {
        public async Task<IActionResult> addAddress(AddressArgs args)
        {
            return await Execute(async () =>
            {
                if (!await repo.roleTb.any(userIdentity.userId, Security.Roles.Rider))
                    return Error(HttpStatusCode.NotFound, Constants.ErrNotFound);

                var addressId = await repo.riderAddressTb.add(userIdentity.userId, new Repository.RiderAddress.RiderAddressTbArgs
                {
                    city = args.City,
                    country = args.Country,
                    state = args.State,
                    street = args.Street
                });
                await repo.usersTb.updateDefaultAddress(userIdentity.userId, addressId);

                return Ok("Your address has been added and set as the default configuration. ");
            });
        }

        public async Task<IActionResult> cancelRideRequest(Guid requestId)
        {
            return await Execute(async () =>
            {
                Guid riderID = userIdentity.userId;

                if (!await repo.roleTb.any(riderID, Security.Roles.Rider))
                    return Error(HttpStatusCode.NotFound, Constants.ErrNotFound);

                var existingRideRequest = await repo.riderRequestTb.getRequest(requestId);

                if (existingRideRequest is null)
                    return Error(HttpStatusCode.NotFound, "Unable to retrieve ride information. Please contact administrator for assistance");

                switch (existingRideRequest.requestStatus.CEnum<RiderRequestStatus>())
                {
                    case RiderRequestStatus.Pending:
                    case RiderRequestStatus.Acknowledged:
                        await repo.riderRequestTb.updateStatus(requestId, RiderRequestStatus.Terminated);
                        break;

                    default:
                        return Error(HttpStatusCode.Forbidden, " It is now impossible to request the cancellation of a ride, as the request has either been canceled already or has been finished.");
                }

                // Send notification to drivers
                // get list of drivers and send out notification
                var drivers = await repo.roleTb.get(Security.Roles.Driver);

                if (drivers is null || !drivers.Any(d => d.activated))
                    return Ok("The request to end the scheduled ride pick-up has been successfully carried out."); // No notification is required to be sent us since no driver is available.

                var riderProfile = await repo.usersTb.get(riderID);
                var pickUpLocation = (await repo.riderAddressTb.get(riderID, existingRideRequest.pickUpAddressID)).CAddressArgs()?.ToString();
                var pickUpDateTime = existingRideRequest.pickUpDateTime.formatDateTime();

                // Send notifications to active drivers only
                foreach (var driver in drivers.Where(d => d.activated))
                {
                    var driverProfile = await repo.usersTb.get(driver.userID);

                    var msg = MailTemplate.rideCancellationMsg(driverProfile.name, pickUpLocation, pickUpDateTime, riderProfile.name);
                    emailServices.postMail(driverProfile.email, msg, "Ride Request Canceled by Rider - No Further Action Required");
                }

                return Ok("The request to end the scheduled ride pick-up has been successfully carried out.");
            });
        }

        public async Task<IActionResult> getDestinationAddress()
        {
            return await Execute(async () =>
            {
                var address = await repo.globalVariableTb.getDefaultDestinationAddress();

                return Ok(address);
            });
        }

        public async Task<IActionResult> removeAddress(Guid addressId)
        {
            return await Execute(async () =>
            {
                if (!await repo.roleTb.any(userIdentity.userId, Security.Roles.Rider))
                    return Error(HttpStatusCode.NotFound, Constants.ErrNotFound);

                var userProfile = await repo.usersTb.get(userIdentity.userId);

                if (userProfile == null)
                    return Error(HttpStatusCode.NotFound, Constants.ErrNotFound);

                await repo.riderAddressTb.del(userIdentity.userId, addressId);

                if (userProfile.defaultAddress == addressId) // Reset default address if the address being delete is the current default one
                    await repo.usersTb.updateDefaultAddress(userIdentity.userId, null);

                return Ok("The specified address has been successfully removed.");
            });
        }

        public async Task<IActionResult> requestForRider(RideRequestArgs args)
        {
            return await Execute(async () =>
            {
                Guid riderID = userIdentity.userId;

                if (!await repo.roleTb.any(riderID, Security.Roles.Rider))
                    return Error(HttpStatusCode.NotFound, Constants.ErrNotFound);

                if (!await repo.riderAddressTb.any(riderID, args.PickUpAddressID))
                    return Error(HttpStatusCode.NotFound, "We are unable to access the pickup location details at this time. Please provide your pickup address again for us to proceed.");

                // validate rider has no active request for the request date and time 
                var existingRides = await repo.riderRequestTb.get(riderID, DateOnly.FromDateTime(args.PickUpDateTime));

                if (existingRides is not null)
                {
                    foreach (var ride in existingRides)
                    {
                        var rideType = ride.requestStatus.CEnum<RiderRequestStatus>();

                        if (rideType is RiderRequestStatus.Pending || rideType is RiderRequestStatus.Acknowledged)
                            return Error(HttpStatusCode.Conflict, "You currently have an active ride within the window of this request");
                    }
                }

                // get list of drivers and send out notification
                var drivers = await repo.roleTb.get(Security.Roles.Driver);

                if (drivers is null || !drivers.Any(d => d.activated))
                    return Error(HttpStatusCode.NotFound, "Unable to complete request. No available driver found yet");

                var requestID = await repo.riderRequestTb.addRideRequest(new Repository.RiderRequest.RiderRequestTbArgs
                {
                    pickUpAddressID = args.PickUpAddressID,
                    pickUpDateTime = args.PickUpDateTime,
                    riderID = riderID
                });

                var riderProfile = await repo.usersTb.get(riderID);
                var acknowledgmentLink = $"{"BASE_URL".getEnvVariable()}/api/driver/acknowledge?ride={requestID}";
                var dropoffLocation = (await repo.globalVariableTb.getDefaultDestinationAddress()).ToString();
                var pickUpLocation = (await repo.riderAddressTb.get(riderID, args.PickUpAddressID)).CAddressArgs()?.ToString();
                var pickUpDateTime = args.PickUpDateTime.formatDateTime();

                // Send notifications to active drivers only
                foreach (var driver in drivers.Where(d => d.activated))
                {
                    var driverProfile = await repo.usersTb.get(driver.userID);

                    var msg = MailTemplate.riderRequestMsg(driverProfile.name, pickUpLocation, dropoffLocation, acknowledgmentLink, pickUpDateTime, riderProfile.name);
                    emailServices.postMail(driverProfile.email, msg, $"New Ride Request Awaiting Your Acknowledgment");
                }

                return Ok("Your ride request has been submitted successfully. Waiting for driver's to acknowledge it");
            });
        }

        public async Task<IActionResult> riderAddresses()
        {
            return await Execute(async () =>
            {
                if (!await repo.roleTb.any(userIdentity.userId, Security.Roles.Rider))
                    return Error(HttpStatusCode.NotFound, Constants.ErrNotFound);

                var userProfile = await repo.usersTb.get(userIdentity.userId);

                if (userProfile == null)
                    return Error(HttpStatusCode.NotFound, Constants.ErrNotFound);

                var addresses = await repo.riderAddressTb.get(userIdentity.userId);

                if (addresses is null)
                    return Error(HttpStatusCode.NotFound, "It appears that there are currently no registered addresses on file. You may add a new address.");

                var addresses_ = addresses.Select(d => new RiderAddressInfo
                {
                    AddressInfo = new AddressArgs(d.street, d.city, d.state, d.zipCode, d.country),
                    DefaultAddress = d.addressID == userProfile.defaultAddress
                });

                return Ok(addresses_);
            });
        }

        public async Task<IActionResult> riderHistory()
        {
            return await Execute(async () =>
            {
                if (!await repo.roleTb.any(userIdentity.userId, Security.Roles.Rider))
                    return Error(HttpStatusCode.NotFound, Constants.ErrNotFound);

                var userProfile = await repo.usersTb.get(userIdentity.userId);

                if (userProfile == null)
                    return Error(HttpStatusCode.NotFound, Constants.ErrNotFound);

                var rides = await repo.riderRequestTb.getAll(userIdentity.userId);

                if (rides is null)
                    return Error(HttpStatusCode.NotFound, "No rider history yet");

                var riderAddresses = await repo.riderAddressTb.get(userIdentity.userId);

                var rideHistory = rides.Select(d => new RideHistoryInfo
                {
                    PickUpDateTime = d.pickUpDateTime.formatDateTime(),
                    PickUpLocation = riderAddresses.FirstOrDefault(x => x.addressID == d.pickUpAddressID).CAddressArgs()?.ToString(),
                    RequestID = d.requestID,
                    Status = d.requestStatus.CEnum<RiderRequestStatus>().ToString()
                });

                return Ok(rideHistory);
            });
        }

        public async Task<IActionResult> updateDefaultAddress(Guid addressId)
        {
            return await Execute(async () =>
            {
                if (!await repo.roleTb.any(userIdentity.userId, Security.Roles.Rider))
                    return Error(HttpStatusCode.NotFound, Constants.ErrNotFound);

                var userProfile = await repo.usersTb.get(userIdentity.userId);

                if (userProfile == null)
                    return Error(HttpStatusCode.NotFound, Constants.ErrNotFound);

                if (addressId == userProfile.defaultAddress)
                    return Error(HttpStatusCode.Conflict, "The selected address is already designated as your default. If you would like to change this, please select another address.");

                if (!await repo.riderAddressTb.any(userIdentity.userId, addressId))
                    return Error(HttpStatusCode.NotFound, "Address not in account.");

                await repo.usersTb.updateDefaultAddress(userIdentity.userId, addressId);

                return Ok("Selected address is now default.");
            });
        }
    }
}
