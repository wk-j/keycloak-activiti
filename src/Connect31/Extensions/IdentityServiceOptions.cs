using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Connect31.Extensions {
    public class IdentityServiceOptions {
        public string Authority { get; internal set; } = string.Empty;
        public string ClientId { get; internal set; } = string.Empty;
        public string ClientSecret { get; internal set; } = string.Empty;
        public string CallbackPath { get; internal set; } = string.Empty;
        public OpenIdConnectEvents? Events { set; internal get; }
    }
}