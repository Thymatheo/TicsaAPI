using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TicsaAPI.Config;

namespace TicsaAPI.Securité {
    public class ApiKeyAuthentificationHandler : AuthenticationHandler<ApiKeyAuthentificationOptions> {
        private const string ApiKeyHeaderName = "X-Api-Key";
        public ApiKeyAuthentificationHandler(IOptionsMonitor<ApiKeyAuthentificationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock) {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
            AppSettingsSection? expectedApiKey = Context.RequestServices.GetRequiredService<IOptions<AppSettingsSection>>().Value;

            if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out Microsoft.Extensions.Primitives.StringValues apiKeyHeaderValues)) {
                return AuthenticateResult.NoResult();
            }

            string? providedApiKey = apiKeyHeaderValues.FirstOrDefault();

            if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey)) {
                return AuthenticateResult.NoResult();
            }

            if (expectedApiKey.ApiKey.Equals(providedApiKey)) {
                List<Claim>? claims = new List<Claim>
            {
                new Claim("ClaimType", "ClaimValue")
            };
                ClaimsIdentity? identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                List<ClaimsIdentity>? identities = new List<ClaimsIdentity> { identity };
                ClaimsPrincipal? principal = new ClaimsPrincipal(identities);
                AuthenticationTicket? ticket = new AuthenticationTicket(principal, Options.Scheme);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Invalid API Key provided.");
        }
    }
}
