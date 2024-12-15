using CSCI_308_TEAM5.API.Services.Config;
using Dapper;
using System.Data.Common;

namespace CSCI_308_TEAM5.API.Repository.GlobalVariable
{
    interface IGlobalVariableTb
    {
        Task<AddressArgs> getDefaultDestinationAddress();
    }

    sealed class GlobalVariableTb(IConfigService configService) : IGlobalVariableTb
    {
        public async Task<AddressArgs> getDefaultDestinationAddress()
        {
            using DbConnection db = configService.dbConnection;
            var data = await db.QueryFirstOrDefaultAsync<GlobalVariableTbModel>(Query.select, new GlobalVariableTbModel
            {
                type = (int)GlobalVariableType.DefaultAddress
            });

            if (data is null)
                return new AddressArgs("Lafayette Rd", "Indianapolis", "Indiana", "46254", "US");

            return data.value.deserializeJson<AddressArgs>();
        }
    }
}
