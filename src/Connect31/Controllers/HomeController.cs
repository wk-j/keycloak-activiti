using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Connect31.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public async Task<ActionResult> Index() {
            if (User.Identity.IsAuthenticated) {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                // var userName = User.FindFirstValue("name"); // null
                // var userName = User.Identity.Name; // null
                var userName = User.Claims.First(x => x.Type == "preferred_username").Value;

                Console.WriteLine(">> Token - {0}", accessToken);
                Console.WriteLine(">> User - {0}", userName);

                foreach (var item in User.Claims) {
                    Console.WriteLine($"== Claim {item.Type} {item.Value}");
                }

                //return Redirect("/index.html");
                return View();
            }
            return Redirect("/Account/Login?returnUrl=/Home/Index");
        }
    }
}