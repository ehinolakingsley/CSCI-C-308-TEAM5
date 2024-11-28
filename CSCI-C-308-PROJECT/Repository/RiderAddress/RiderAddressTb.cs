using CSCI_308_TEAM5.API.Services.Config;

namespace CSCI_308_TEAM5.API.Repository.RiderAddress
{
    interface IRiderAddressTb
    {
        Task<Guid> add(Guid userId, RiderAddressTbArgs args);

        Task update(Guid addressId, RiderAddressTbArgs args);
    }

    sealed class RiderAddressTb(IConfigService configService) : IRiderAddressTb
    {
        public Task<Guid> add(Guid userId, RiderAddressTbArgs args)
        {
            throw new NotImplementedException();
        }

        public Task update(Guid addressId, RiderAddressTbArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
