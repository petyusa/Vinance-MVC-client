using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Account
{
    using Contracts.Interfaces;

    public class GetAllAccount : ViewComponent
    {
        private readonly IAccountApi _accountApi;

        public GetAllAccount(IAccountApi accountApi)
        {
            _accountApi = accountApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool editable)
        {
            var accounts = await _accountApi.GetAll();
            ViewBag.Editable = editable;
            return View("GetAllAccount", accounts);
        }
    }
}