using Microsoft.AspNetCore.Authentication;

namespace TicsaAPI.Securité {
    public class ApiKeyAuthentificationOptions : AuthenticationSchemeOptions {
        public const string DefaultHeaderName = "X-Api-Key";
        public string HeaderName { get; set; } = DefaultHeaderName;

        public const string DefaultScheme = "API Key";
        public string Scheme => DefaultScheme;

        public string AuthenticationType = DefaultScheme;
    }
}
