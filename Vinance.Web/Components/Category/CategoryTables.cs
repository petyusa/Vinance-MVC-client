using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Category
{
    public class CategoryTables : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("CategoryTables");
        }
    }
}