using CSCI_308_TEAM5.API.Extensions;

namespace CSCI_308_TEAM5.API.Repository.Authentication
{
    static class Query
    {
        const string TableName = "";

        internal static string Insert => TableName.getInsertQuery<AuthenticationTbModel>();

    }
}
