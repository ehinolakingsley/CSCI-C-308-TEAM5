namespace CSCI_308_TEAM5.API.Extensions
{
    public static class ConverterExtensions
    {
        public static T CEnum<T>(this string value) where T : Enum
        {
            if (value.empty())
                return default;

            if (!Enum.TryParse(typeof(T), value, ignoreCase: true, out object result))
                return default;

            return (T)result;
        }

        public static Guid CGuid(this string value)
        {
            if (!Guid.TryParse(value, out Guid result))
            {
                if (value != Guid.Empty.ToString())
                    throw new ArgumentException($"Parsed GUID ({value}) is invalid!");

                return Guid.Empty;
            }

            return result;
        }

    }
}
