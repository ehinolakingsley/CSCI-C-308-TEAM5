using CSCI_308_TEAM5.API.Repository.Role;
using CSCI_308_TEAM5.API.Security;
using CSCI_308_TEAM5.API.Services.Config;
using Dapper;
using System.Data.Common;

namespace CSCI_308_TEAM5.API.Repository.Authentication
{
    interface IAuthenticationTb
    {
        Task<AuthenticationTbModel> get(Guid userId, int roleId);

        Task del(Guid userId, Roles roleId);

        Task addOrUpdate(AuthenticationTbArgs args);
    }

    sealed class AuthenticationTb(IConfigService configService) : IAuthenticationTb
    {
        public async Task addOrUpdate(AuthenticationTbArgs args)
        {
            var payload = new AuthenticationTbModel
            {
                dateCreated = DateTime.UtcNow,
                expires = args.expires,
                refreshToken = args.refreshToken,
                roleId = args.roleId,
                token = args.token,
                userId = args.userId
            };

            using DbConnection db = configService.dbConnection;
            if (await db.QueryFirstOrDefaultAsync<bool>(Query.anyRecord, payload))
                await db.ExecuteAsync(Query.del, payload);
            else
                await db.ExecuteAsync(Query.insert, payload);
        }

        public async Task del(Guid userId, Roles roleId)
        {
            using DbConnection db = configService.dbConnection;
            await db.ExecuteAsync(Query.del, new AuthenticationTbModel
            {
                userId = userId,
                roleId = (int)roleId
            });
        }

        public async Task<AuthenticationTbModel> get(Guid userId, int roleId)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryFirstOrDefaultAsync<AuthenticationTbModel>(Query.selectRecord, new AuthenticationTbModel
            {
                userId = userId,
                roleId = roleId
            });
        }
    }
}
