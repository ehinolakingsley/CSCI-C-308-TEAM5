namespace CSCI_308_TEAM5.API.Repository.Role
{
    static class Query
    {
        const string tableName = "role";

        internal static string insert => tableName.getInsertQuery<RoleTbModel>();

        internal static string anyRecord => $"SELECT CASE WHEN EXISTS (SELECT 1 FROM {tableName} WHERE userID = @userID AND roleID = @roleID) THEN 1 ELSE 0 END";

        internal static string select => $"SELECT * FROM {tableName} WHERE userID = @userID AND roleID = @roleID";
    }
}
