using System;
using System.Threading.Tasks;
using Connect31.Extensions;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;

namespace Connect31 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            IdentityModelEventSource.ShowPII = true;

            services.AddControllersWithViews();

            var settings = new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            services.AddIdentityService(new IdentityServiceOptions {
                Authority = Configuration["Options:Authority"],
                ClientId = Configuration["Options:ClientId"],
                ClientSecret = Configuration["Options:ClientSecret"],
                CallbackPath = new PathString("/login-123-abc-xyz"),
                Events = new OpenIdConnectEvents {
                    OnTokenResponseReceived = context => {
                        var ticket = context.ProtocolMessage.AccessToken;
                        return Task.CompletedTask;
                    },
                    OnUserInformationReceived = x => {
                        // var json = JsonConvert.SerializeObject(x.Principal.Claims, Formatting.Indented, settings);
                        // Console.WriteLine(json);
                        return Task.CompletedTask;
                    }
                }
            });
            services.AddMvcCore();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
