using CSCI_308_TEAM5.API.Repository.RiderAddress;
using CSCI_308_TEAM5.API.Services.Config;
using Dapper;
using System.Data.Common;

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
        public async Task<Guid> addRideRequest(RiderRequestTbArgs args)
        {
            var id = Guid.NewGuid();

            using DbConnection db = configService.dbConnection;
            await db.ExecuteAsync(Query.insert, new RiderRequestTbModel
            {
                assignedDriverID = args.pickUpAddressID,
                requestID = id,
                dateCreated = DateTime.UtcNow,
                lastModified = DateTime.UtcNow,
                pickUpAddressID = args.pickUpAddressID,
                pickUpDateTime = args.pickUpDateTime,
                requestStatus = (int)RiderRequestStatus.Pending,
                riderID = args.riderID
            });

            return id;
        }

        public async Task<IEnumerable<RiderRequestTbModel>> get(Guid riderID, DateOnly rideDate)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryAsync<RiderRequestTbModel>(Query.select, new
            {
                riderID = riderID,
                pickUpDateTime = rideDate.ToString()
            });
        }

        public async Task<IEnumerable<RiderRequestTbModel>> getAll(Guid riderID)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryAsync<RiderRequestTbModel>(Query.selectAll, new RiderRequestTbModel
            {
                riderID = riderID
            });
        }

        public async Task<RiderRequestTbModel> getRequest(Guid requestID)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryFirstOrDefaultAsync<RiderRequestTbModel>(Query.selectByID, new RiderRequestTbModel
            {
                requestID = requestID
            });
        }

        public async Task updateStatus(Guid requestID, RiderRequestStatus status)
        {
            using DbConnection db = configService.dbConnection;
            await db.ExecuteAsync(Query.updateStatus, new RiderRequestTbModel
            {
                requestID = requestID,
                lastModified = DateTime.UtcNow,
                requestStatus = (int)status
            });
        }
    }
}
