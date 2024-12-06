using CSCI_308_TEAM5.API.Services.Config;
using Dapper;
using System.Data.Common;

namespace CSCI_308_TEAM5.API.Repository.OneTimeCode
{
    interface IOneTimeCodeTb
    {
        Task addOrUpdate(OneTimeTbArgs args);

        Task<OneTimeTbModel> get(int tokenCode);

        Task del(int tokenCode);
    }

    sealed class OneTimeCodeTb(IConfigService configService) : IOneTimeCodeTb
    {
        public async Task addOrUpdate(OneTimeTbArgs args)
        {
            var payload = new OneTimeTbModel
            {
                DateCreated = DateTime.UtcNow,
                Expires = args.Expires,
                RoleId = args.RoleId,
                OTP = args.OTP,
                UserId = args.UserId
            };

            using DbConnection db = configService.dbConnection;
            if (await db.QueryFirstOrDefaultAsync<bool>(Query.anyRecord, payload))
                await db.ExecuteAsync(Query.delRecord, payload);
            else
                await db.ExecuteAsync(Query.insert, payload);
        }

        public async Task del(int tokenCode)
        {
            using DbConnection db = configService.dbConnection;
            await db.ExecuteAsync(Query.delOTP, new OneTimeTbModel
            {
                OTP = tokenCode
            });
        }

        public async Task<OneTimeTbModel> get(int tokenCode)
        {
            using DbConnection db = configService.dbConnection;
            return await db.QueryFirstOrDefaultAsync<OneTimeTbModel>(Query.selectRecord, new OneTimeTbModel
            {
                OTP = tokenCode
            });
        }
    }
}
