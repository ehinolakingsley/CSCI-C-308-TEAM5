using CSCI_308_TEAM5.API.Security;
using CSCI_308_TEAM5.API.Services.Config;
using Dapper;
using System.Data.Common;

namespace CSCI_308_TEAM5.API.Repository.Role
{
    interface IRoleTb
    {
        Task<bool> any(Guid userId, Roles role);

        Task add(Guid userId, Roles role, bool activate);

        Task updateStatus(Guid userId, Roles role, bool status);

        Task<RoleTbModel> get(Guid userId, Roles role);
    }

    sealed class RoleTb(IConfigService configService) : IRoleTb
    {
        public async Task add(Guid userId, Roles role, bool activate)
        {
            using DbConnection db = configService.dbConnection;
            await db.ExecuteAsync(Query.insert, new RoleTbModel
            {
                userID = userId,
                activated = activate,
                role = role.ToString(),
                roleID = (int)role
            });
        }

        public async Task<bool> any(Guid userId, Roles role)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryFirstOrDefaultAsync<bool>(Query.anyRecord, new RoleTbModel { userID = userId, roleID = (int)role });
        }

        public async Task<RoleTbModel> get(Guid userId, Roles role)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryFirstOrDefaultAsync<RoleTbModel>(Query.select, new RoleTbModel { userID = userId, roleID = (int)role });
        }

        public Task updateStatus(Guid userId, Roles role, bool status)
        {
            throw new NotImplementedException();
        }
    }
}
