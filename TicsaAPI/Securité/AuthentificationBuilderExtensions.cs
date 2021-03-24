using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicsaAPI.Securité
{
    public static class AuthentificationBuilderExtensions
    {
        public static AuthenticationBuilder AddApiKeySupport(this AuthenticationBuilder authenticationBuilder, Action<ApiKeyAuthentificationOptions> options)
        {
            return authenticationBuilder.AddScheme<ApiKeyAuthentificationOptions, ApiKeyAuthentificationHandler>(ApiKeyAuthentificationOptions.DefaultScheme, options);
        }
    }
}
