using Microsoft.AspNetCore.Authentication;

namespace CSCI_308_TEAM5.API.Security
{
    public static class AddSecurity
    {
        public static AuthenticationBuilder AddTeam5Jwt(this AuthenticationBuilder builder) => builder.AddScheme<AuthenticationSchemeOptions, Authenticator>("Bearer", null, null);

        public static void AddSecurities(this IServiceCollection services)
        {
            services.AddScoped<IUserIdentity, UserIdentity>();
        }
    }
}
