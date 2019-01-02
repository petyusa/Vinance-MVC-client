using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Account
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;

    public class GetAllAccount : ViewComponent
    {
        private readonly IAccountApi _accountApi;

        public GetAllAccount(IAccountApi accountApi)
        {
            _accountApi = accountApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(AccountType? accountType, bool editable)
        {
            var accounts = await _accountApi.GetAll(accountType);
            ViewBag.Editable = editable;
            ViewBag.AccountType = accountType;
            accounts = accounts.OrderBy(a => a.Name);
            return View("GetAllAccount", accounts);
        }
    }
}