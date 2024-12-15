using CSCI_308_TEAM5.API.Services.Config;
using Dapper;
using System.Data.Common;

namespace CSCI_308_TEAM5.API.Repository.RiderAddress
{
    interface IRiderAddressTb
    {
        Task<Guid> add(Guid userId, RiderAddressTbArgs args);

        Task update(Guid addressId, RiderAddressTbArgs args);

        Task del(Guid userId, Guid addressId);

        Task<IEnumerable<RiderAddressTbModel>> get(Guid userId);

        Task<RiderAddressTbModel> get(Guid userId, Guid addressId);

        Task<bool> any(Guid userId, Guid addressId);
    }

    sealed class RiderAddressTb(IConfigService configService) : IRiderAddressTb
    {
        public async Task<Guid> add(Guid userId, RiderAddressTbArgs args)
        {
            var id = Guid.NewGuid();

            using DbConnection db = configService.dbConnection;
            await db.ExecuteAsync(Query.insert, new RiderAddressTbModel
            {
                addressID = id,
                userID = userId,
                city = args.city,
                country = args.country,
                dateCreated = DateTime.UtcNow,
                state = args.state,
                street = args.street,
                DELETED = false,
                zipCode = args.zipCode,
            });

            return id;
        }

        public async Task<bool> any(Guid userId, Guid addressId)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryFirstOrDefaultAsync<bool>(Query.anyAddress, new RiderAddressTbModel
            {
                addressID = addressId,
                userID = userId,
                DELETED = false
            });
        }

        public async Task del(Guid userId, Guid addressId)
        {
            using DbConnection db = configService.dbConnection;
            await db.ExecuteAsync(Query.deleteAddress, new RiderAddressTbModel
            {
                addressID = addressId,
                userID = userId,
                DELETED = true // Persist record in the DB to utilize for audit sake
            });
        }

        public async Task<IEnumerable<RiderAddressTbModel>> get(Guid userId)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryAsync<RiderAddressTbModel>(Query.selectByUserID, new RiderAddressTbModel
            {
                userID = userId,
                DELETED = false
            });
        }

        public async Task<RiderAddressTbModel> get(Guid userId, Guid addressId)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryFirstOrDefaultAsync<RiderAddressTbModel>(Query.selectByAddressID, new RiderAddressTbModel
            {
                userID = userId,
                addressID = addressId,
                DELETED = false
            });
        }

        public async Task update(Guid addressId, RiderAddressTbArgs args)
        {
            using DbConnection db = configService.dbConnection;
            await db.ExecuteAsync(Query.update, new RiderAddressTbModel
            {
                addressID = addressId,
                DELETED = false,
                city = args.city,
                country = args.country,
                state = args.state,
                street = args.street,
                zipCode = args.zipCode
            });
        }
    }
}
