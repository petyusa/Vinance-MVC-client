using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Account
{
    using Contracts.Interfaces;

    public class EditAccountInTable : ViewComponent
    {
        private readonly IAccountApi _accountApi;

        public EditAccountInTable(IAccountApi accountApi)
        {
            _accountApi = accountApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(int accountId)
        {
            var account = await _accountApi.GetById(accountId);
            return View("EditAccountInTable", account);
        }
    }
}