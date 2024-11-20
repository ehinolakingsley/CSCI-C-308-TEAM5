using System.Collections;

namespace CSCI_308_TEAM5.API
{
    public static class Extensions
    {
        public static bool IsEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            value = value.Trim();
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            return value.Length == 0;
        }

        public static T CEnum<T>(this string value) where T : Enum
        {
            if (value.IsEmpty())
                return default;

            if (!Enum.TryParse(typeof(T), value, ignoreCase: true, out object result))
                return default;

            return (T)result;
        }


        public static string getEnvVariable(this string envVariable, bool throwExceptionIfEmpty = false)
        {
            string environmentVariable = Environment.GetEnvironmentVariable(envVariable);
            if (environmentVariable.IsEmpty() && throwExceptionIfEmpty)
            {
                // try lower case key
                foreach (DictionaryEntry environmentVariable2 in Environment.GetEnvironmentVariables())
                    if (environmentVariable2.Key.ToString().ToLower() == envVariable.ToLower())
                        return environmentVariable2.Value?.ToString();

                throw new ArgumentNullException("Environment variable for '" + envVariable + "' is required!");
            }

            return environmentVariable;
        }
    }
}
