using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Account
{
    public class CreateAccountInTable : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("CreateAccountInTable");
        }
    }
}