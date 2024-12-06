namespace CSCI_308_TEAM5.API.Extensions
{
    public static class DBExtensions
    {
        public static string getInsertQuery<T>(this string tableName)
        {
            var properties = typeof(T).GetProperties();

            if (properties is null)
                throw new ArgumentNullException("No properties found in the selected model.");

            var columns = string.Join(", ", properties.Select(prop => $"\"{prop.Name}\""));
            var values = string.Join(", ", properties.Select(prop => $"@{prop.Name}"));

            return $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
        }
    }
}
