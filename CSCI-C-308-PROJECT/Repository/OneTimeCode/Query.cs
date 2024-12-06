namespace CSCI_308_TEAM5.API.Repository.OneTimeCode
{
    static class Query
    {
        const string tableName = "oneTimeCode";

        internal static string delOTP => $"DELETE FROM {tableName} WHERE OTP = @OTP";

        internal static string delRecord => $"DELETE FROM {tableName} WHERE userID = @userID AND roleID = @roleID";

        internal static string selectRecord => $"SELECT * FROM {tableName} WHERE OTP = @OTP";

        internal static string anyRecord => $"SELECT CASE WHEN EXISTS (SELECT 1 FROM {tableName} WHERE userID = @userID AND roleID = @roleID) THEN 1 ELSE 0 END";

        internal static string insert => tableName.getInsertQuery<OneTimeTbModel>();
    }
}
