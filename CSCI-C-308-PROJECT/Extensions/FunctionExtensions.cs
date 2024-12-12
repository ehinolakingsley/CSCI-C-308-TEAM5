using CSCI_308_TEAM5.API.Security;
using System.Collections;
using System.Security.Claims;
using System.Text.Json;

namespace CSCI_308_TEAM5.API.Extensions
{
    public static class FunctionExtensions
    {
        public static bool stringJson(this string value)
        {
            try
            {
                _ = JsonDocument.Parse(value); return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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

        public static bool empty(this Guid? value)
        {
            if (value is null)
                return true;

            return value == Guid.Empty;
        }

        public static string parseSchemedToken(this string token, string scheme = "Bearer")
        {
            var tokens = token.ToString().Split(' ');

            if (tokens.Length == 2 && tokens[0] == scheme && !tokens[1].empty())
                return tokens[1];

            throw new ArgumentException($"Unable to parse scheme type: '{scheme}' with value: '{token}'");
        }

        public static IUserIdentity parseClaims(this IEnumerable<Claim> claims, string userAgent)
        {
            if (claims is null)
                return null;

            return new UserIdentity
            {
                username = claims.FirstOrDefault(i => i.Type == Constants.jwtClaimUsername)?.Value,
                accessRole = claims.FirstOrDefault(i => i.Type == Constants.jwtClaimRoleID)?.Value.CEnum<Roles>() ?? Roles.None,
                userId = claims.FirstOrDefault(i => i.Type == Constants.jwtClaimUserID)?.Value.CGuid() ?? Guid.Empty,
                userAgent = userAgent
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

        public static string formatName(this string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new ArgumentException("Full name cannot be empty.", nameof(fullName));

            var parts = fullName.Split([' '], StringSplitOptions.RemoveEmptyEntries);

            string firstName = parts[0];

            string formattedFullName = $"{char.ToUpper(firstName[0]) + firstName[1..].ToLower()}";

            for (int i = 1; i < parts.Length; i++)
            {
                string part = parts[i];

                // Capitalize the first letter if it's not already capitalized
                if (!char.IsUpper(part[0]))
                    formattedFullName += " " + char.ToUpper(part[0]) + part[1..].ToLower();
                else
                    formattedFullName += " " + part;
            }

            return formattedFullName;
        }

        public static int generateOneTime6DigitCode(this Random random) => random.Next(100000, 1000000);

        public static string maskEmail(this string email)
        {
            var emailParts = email.Split('@');
            return emailParts[0].Substring(0, 1) + "*****@" + emailParts[1];
        }
    }
}
