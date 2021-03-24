using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicsaAPI.Securité
{
    public class ApiKeyAuthentificationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultHeaderName = "X-Api-Key";
        public string HeaderName { get; set; } = DefaultHeaderName;

        public const string DefaultScheme = "API Key";
        public string Scheme => DefaultScheme;

        public string AuthenticationType = DefaultScheme;
    }
}
