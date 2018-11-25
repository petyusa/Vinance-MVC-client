using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.MainPage
{
    public class MainPageTables : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("MainPageTables");
        }
    }
}