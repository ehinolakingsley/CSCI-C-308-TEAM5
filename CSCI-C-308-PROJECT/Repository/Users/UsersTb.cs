using CSCI_308_TEAM5.API.Services.Config;
using Dapper;
using System.Data.Common;

namespace CSCI_308_TEAM5.API.Repository.Users
{
    interface IUsersTb
    {
        Task<UsersTbModel> get(string emailAddress);

        Task<UsersTbModel> get(Guid userId);

        Task update(Guid userId, UsersTbArgs args);

        Task<Guid> add(UsersTbArgs args);
    }

    sealed class UsersTb(IConfigService configService) : IUsersTb
    {
        public async Task<Guid> add(UsersTbArgs args)
        {
            var id = Guid.NewGuid();

            using DbConnection db = configService.dbConnection;
            await db.ExecuteAsync(Query.insert, new UsersTbModel
            {
                DateCreated = DateTime.UtcNow,
                Email = args.Email?.ToLower(),
                LastModified = DateTime.UtcNow,
                Name = args.Name,
                Phone = args.Phone,
                UserId = id
            });

            return id;
        }

        public async Task<UsersTbModel> get(string emailAddress)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryFirstOrDefaultAsync<UsersTbModel>(Query.selectByEmail, new UsersTbModel
            {
                Email = emailAddress?.ToLower()
            });
        }

        public async Task<UsersTbModel> get(Guid userId)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryFirstOrDefaultAsync<UsersTbModel>(Query.selectById, new UsersTbModel
            {
                UserId = userId
            });
        }

        public async Task update(Guid userId, UsersTbArgs args)
        {
            using DbConnection db = configService.dbConnection;
            await db.ExecuteAsync(Query.updateRecord, new UsersTbModel
            {
                Email = args.Email?.ToLower(),
                LastModified = DateTime.UtcNow,
                Name = args.Name,
                Phone = args.Phone,
                UserId = userId
            });
        }
    }
}
