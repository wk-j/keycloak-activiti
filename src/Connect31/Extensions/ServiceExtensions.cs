using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Connect31.Extensions {
    public static class ServiceExtensions {
        public static void AddIdentityService(this IServiceCollection services, IdentityServiceOptions opt) {
            services
              .AddAuthentication(options => {
                  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
              })
              .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, (options) => {
              options.Authority = opt.Authority;
              options.ClientId = opt.ClientId;
              options.ClientSecret = opt.ClientSecret;
              options.CallbackPath = new PathString(opt.CallbackPath);
              options.ResponseType = OpenIdConnectResponseType.CodeIdTokenToken;
              options.RequireHttpsMetadata = false;
              options.SaveTokens = true;
              options.Events = opt.Events;
              options.GetClaimsFromUserInfoEndpoint = true;
          });
        }
    }
}
