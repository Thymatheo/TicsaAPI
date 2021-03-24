﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TicsaAPI.Config;

namespace TicsaAPI.Securité
{
    public class ApiKeyAuthentificationHandler : AuthenticationHandler<ApiKeyAuthentificationOptions>
    {
        private const string ApiKeyHeaderName = "X-Api-Key";
        public ApiKeyAuthentificationHandler(IOptionsMonitor<ApiKeyAuthentificationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var expectedApiKey = Context.RequestServices.GetRequiredService<IOptions<AppSettingsSection>>().Value;

            if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
            {
                return AuthenticateResult.NoResult();
            }

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

            if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey))
            {
                return AuthenticateResult.NoResult();
            }

            if (expectedApiKey.ApiKey.Equals(providedApiKey))
            {
                var claims = new List<Claim>
            {
                new Claim("ClaimType", "ClaimValue")
            };
                var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                var identities = new List<ClaimsIdentity> { identity };
                var principal = new ClaimsPrincipal(identities);
                var ticket = new AuthenticationTicket(principal, Options.Scheme);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Invalid API Key provided.");
        }
    }
}
