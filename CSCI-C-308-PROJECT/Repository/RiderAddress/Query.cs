namespace CSCI_308_TEAM5.API.Repository.RiderAddress
{
    static class Query
    {
        const string tableName = "riderAddress";

        internal static string insert => tableName.getInsertQuery<RiderAddressTbModel>();
    }
}
