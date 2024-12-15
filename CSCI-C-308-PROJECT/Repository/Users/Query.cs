namespace CSCI_308_TEAM5.API.Repository.Users
{
    static class Query
    {
        const string tableName = "users";

        internal static string insert => tableName.getInsertQuery<UsersTbModel>();

        internal static string selectByEmail => $"SELECT * FROM {tableName} WHERE email = @email";

        internal static string selectById => $"SELECT * FROM {tableName} WHERE \"userID\" = @userID";

        internal static string updateRecord => $"UPDATE {tableName} SET name = @name, phone = @phone, email = @email, \"lastModified\" = @lastModified WHERE \"userID\" = @userID";

        internal static string updateDefaultAddress => $"UPDATE {tableName} SET defaultAddress = @defaultAddress, \"lastModified\" = @lastModified WHERE \"userID\" = @userID";
    }
}
