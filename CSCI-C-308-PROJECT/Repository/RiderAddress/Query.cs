namespace CSCI_308_TEAM5.API.Repository.RiderAddress
{
    static class Query
    {
        const string tableName = "riderAddress";

        internal static string insert => tableName.getInsertQuery<RiderAddressTbModel>();

        internal static string anyAddress => $"SELECT CASE WHEN EXISTS (SELECT 1 FROM {tableName} WHERE \"userID\" = @userID AND \"addressID\" = @addressID AND \"DELETED\" = @DELETED) THEN 1 ELSE 0 END";

        internal static string deleteAddress => $"UPDATE {tableName} SET \"DELETED\" = @DELETED WHERE \"userID\" = @userID AND \"addressID\" = @addressID";

        internal static string selectByUserID => $"SELECT * FROM {tableName} WHERE \"userID\" = @userID AND \"DELETED\" = @DELETED";

        internal static string selectByAddressID => $"SELECT * FROM {tableName} WHERE \"userID\" = @userID AND \"DELETED\" = @DELETED AND \"addressID\" = @addressID";

        internal static string update => $"UPDATE {tableName} SET \"DELETED\" = @DELETED, city = @city, country = @country, state = @state, street = @street, zipCode = @zipCode WHERE \"addressID\" = @addressID";
    }
}
