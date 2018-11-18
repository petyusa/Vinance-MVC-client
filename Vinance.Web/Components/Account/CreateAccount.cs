using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Account
{
    public class CreateAccount : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("CreateAccount");
        }
    }
}
