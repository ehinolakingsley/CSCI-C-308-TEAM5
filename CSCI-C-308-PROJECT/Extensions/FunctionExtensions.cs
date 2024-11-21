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
    }
}
