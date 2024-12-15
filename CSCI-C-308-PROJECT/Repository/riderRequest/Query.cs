namespace CSCI_308_TEAM5.API.Repository.RiderRequest
{
    static class Query
    {
        const string tableName = "riderRequest";

        internal static string insert => tableName.getInsertQuery<RiderRequestTbModel>();

        internal static string select => $"SELECT * FROM {tableName} WHERE \"riderID\" = @riderID AND CAST(\"pickUpDateTime\" AS DATE) = @pickUpDateTime";

        internal static string selectAll => $"SELECT * FROM {tableName} WHERE \"riderID\" = @riderID";

        internal static string selectByID => $"SELECT * FROM {tableName} WHERE \"requestID\" = @requestID";

        internal static string updateStatus => $"UPDATE {tableName} SET \"lastModified\" = @lastModified, \"requestStatus\" = @requestStatus WHERE \"requestID\" = @requestID";

    }
}
