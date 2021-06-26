using Microsoft.AspNetCore.Authentication;
using System;

namespace TicsaAPI.Securité {
    public static class AuthentificationBuilderExtensions {
        public static AuthenticationBuilder AddApiKeySupport(this AuthenticationBuilder authenticationBuilder, Action<ApiKeyAuthentificationOptions> options) {
            return authenticationBuilder.AddScheme<ApiKeyAuthentificationOptions, ApiKeyAuthentificationHandler>(ApiKeyAuthentificationOptions.DefaultScheme, options);
        }
    }
}
