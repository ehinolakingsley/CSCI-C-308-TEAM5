using CSCI_308_TEAM5.API.Services.Config;
using Dapper;
using System.Data.Common;

namespace CSCI_308_TEAM5.API.Repository.RiderAddress
{
    interface IRiderAddressTb
    {
        Task<Guid> add(Guid userId, RiderAddressTbArgs args);

        Task update(Guid addressId, RiderAddressTbArgs args);
    }

    sealed class RiderAddressTb(IConfigService configService) : IRiderAddressTb
    {
        public async Task<Guid> add(Guid userId, RiderAddressTbArgs args)
        {
            var id = Guid.NewGuid();

            using DbConnection db = configService.dbConnection;
            await db.ExecuteAsync(Query.insert, new RiderAddressTbModel
            {
                AddressId = id,
                UserId = userId,
                City = args.City,
                Country = args.Country,
                DateCreated = DateTime.UtcNow,
                State = args.State,
                Street = args.Street
            });

            return id;
        }

        public Task update(Guid addressId, RiderAddressTbArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
