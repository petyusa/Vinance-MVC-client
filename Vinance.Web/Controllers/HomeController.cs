using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Controllers
{
    using Models;
    using Services.APIs;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly VinanceApi _vinanceApi;

        public HomeController(VinanceApi vinanceApi)
        {
            _vinanceApi = vinanceApi;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string About()
        {
            ViewData["Message"] = "Your application description page.";

            return "haha";
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
