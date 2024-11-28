using CSCI_308_TEAM5.API.Extensions;
using CSCI_308_TEAM5.API.Services.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Encodings.Web;

namespace CSCI_308_TEAM5.API.Security
{
    sealed class Authenticator(IConfigService configService, IUserIdentity userIdentity, IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out var tokenInfo);

                var jwtToken = tokenInfo.ToString().parseSchemedToken(Scheme.Name);

                var principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidIssuer = "csci-team5-members",
                    ValidAudience = "Transportation Users",
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configService.jwtSignature))
                }, out _);

                Request.Headers.TryGetValue("UserAgent", out var userAgent);
                var identity = principal.Claims.parseClaims(userAgent);

                userIdentity.update(identity);

                return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name)));
            }
            catch (Exception ex)
            {
                return Task.FromResult(AuthenticateResult.Fail(ex.Message));
            }
        }

    }
}
