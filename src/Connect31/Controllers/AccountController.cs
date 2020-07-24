using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Connect31 {
    public class AccountController : Controller {
        public async Task Login(string returnUrl = "/home/index") {
            await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties() {
                RedirectUri = returnUrl
            });
        }

        public async Task<ActionResult> Logout(string returnUrl = "https://localhost:5001/account/login") {
            await HttpContext.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}