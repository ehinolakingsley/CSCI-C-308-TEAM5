namespace CSCI_308_TEAM5.API.Repository.GlobalVariable
{
    static class Query
    {
        const string tableName = "globalVariable";

        internal static string select => $"SELECT * FROM {tableName} WHERE type = @type";

    }
}
