using CSCI_308_TEAM5.API.Security;
using System.Collections;
using System.Security.Claims;

namespace CSCI_308_TEAM5.API.Extensions
{
    public static class FunctionExtensions
    {
        public static bool empty(this string value)
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

        public static string parseSchemedToken(this string token, string scheme = "Bearer")
        {
            var tokens = token.ToString().Split(' ');

            if (tokens.Length == 2 && tokens[0] == scheme && !tokens[1].empty())
                return tokens[1];

            throw new ArgumentException($"Unable to parse scheme type: '{scheme}' with value: '{token}'");
        }

        public static IUserIdentity parseClaims(this IEnumerable<Claim> claims)
        {
            if (claims is null)
                return null;

            return new UserIdentity
            {
                username = claims.FirstOrDefault(i => i.Type == "username")?.Value,
                accessRole = claims.FirstOrDefault(i => i.Type == "role")?.Value.CEnum<Roles>() ?? Roles.None,
                userId = claims.FirstOrDefault(i => i.Type == "id")?.Value.CGuid() ?? Guid.Empty
            };
        }

        public static string getEnvVariable(this string envVariable, bool throwExceptionIfEmpty = false)
        {
            string environmentVariable = Environment.GetEnvironmentVariable(envVariable);
            if (environmentVariable.empty() && throwExceptionIfEmpty)
            {
                // try lower case key
                foreach (DictionaryEntry environmentVariable2 in Environment.GetEnvironmentVariables())
                    if (environmentVariable2.Key.ToString().ToLower() == envVariable.ToLower())
                        return environmentVariable2.Value?.ToString();

                throw new ArgumentNullException("Environment variable for '" + envVariable + "' is required!");
            }

            return environmentVariable;
        }

        public static bool addressValid(this AddressArgs address, out string message)
        {
            message = "Please enter your full address including house number, street, city, state, and ZIP code if applicable.";

            if (address is null)
                return false;

            bool hasValidLenght(string value, int min, int max)
            {
                int length = value.Length;
                return length >= min && length <= max;
            }

            return (message = address switch
            {
                _ when address.Street.empty() => "Street address cannot be left blank...",
                _ when !hasValidLenght(address.Street, 5, 300) => "Street address length must be between 5 and 300 characters.",
                _ when address.State.empty() => "State cannot be left blank...",
                _ when !hasValidLenght(address.State, 2, 50) => "State length must be between 2 and 50 characters.",
                _ when address.City.empty() => "City cannot be left blank...",
                _ when !hasValidLenght(address.City, 2, 50) => "City length must be between 2 and 50 characters.",
                _ when address.Country.empty() => "Country cannot be left blank...",
                _ when !hasValidLenght(address.Country, 2, 50) => "Country length must be between 2 and 50 characters.",
                _ when !address.ZipCode.empty() && !hasValidLenght(address.ZipCode, 2, 10) => "Zip-Code length must be between 2 and 10 characters.",
                _ => "valid"

            }) == "valid";
        }
    }
}
