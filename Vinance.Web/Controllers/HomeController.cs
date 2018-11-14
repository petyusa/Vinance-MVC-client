using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vinance.Contracts.Interfaces;
using Vinance.Services;
using Vinance.Web.Models;

namespace Vinance.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly VinanceApi _vinanceApi;
        private readonly IUserApi _userApi;

        public HomeController(VinanceApi vinanceApi, IUserApi userApi)
        {
            _vinanceApi = vinanceApi;
            _userApi = userApi;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<string> About()
        {
            ViewData["Message"] = "Your application description page.";
            
            return "haha";
        }

        public IActionResult Login()
        {
            return View("Index");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
