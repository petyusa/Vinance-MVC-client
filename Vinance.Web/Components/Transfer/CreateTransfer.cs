using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Transfer
{
    using Contracts.Interfaces;
    using Models;

    public class CreateTransfer : ViewComponent
    {
        private readonly IAccountApi _accountApi;

        public CreateTransfer(IAccountApi accountApi)
        {
            _accountApi = accountApi;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var accounts = await _accountApi.GetAll();
            var model = new CreateTransferViewmodel
            {
                AccountList = accounts.Where(a => a.IsActive).Select(a => new SelectListItem(a.Name, a.Id.ToString()))
            };
            return View("CreateTransfer", model);
        }
    }
}