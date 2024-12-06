namespace CSCI_308_TEAM5.API.Repository.Authentication
{
    static class Query
    {
        const string tableName = "role";

        internal static string del => $"DELETE FROM {tableName} WHERE \"userID\" = @userID AND \"roleID\" = @roleID";

        internal static string selectRecord => $"SELECT * FROM {tableName} WHERE \"userID\" = @userID AND \"roleID\" = @roleID";

        internal static string anyRecord => $"SELECT CASE WHEN EXISTS (SELECT 1 FROM {tableName} WHERE \"userID\" = @userID AND \"roleID\" = @roleID) THEN 1 ELSE 0 END";

        internal static string insert => tableName.getInsertQuery<AuthenticationTbModel>();
    }
}
