using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Category
{
    public class CreateCategory : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("CreateCategory");
        }
    }
}