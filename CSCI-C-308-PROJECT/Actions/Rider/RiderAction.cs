using CSCI_308_TEAM5.API.Actions.BaseAction;

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

        public Task<IActionResult> cancelRideRequest(Guid requestId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> getDestinationAddress()
        {
            throw new NotImplementedException();
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

        public Task<IActionResult> requestForRider(Guid addressId)
        {
            throw new NotImplementedException();
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

        public Task<IActionResult> riderHistory()
        {
            throw new NotImplementedException();
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
