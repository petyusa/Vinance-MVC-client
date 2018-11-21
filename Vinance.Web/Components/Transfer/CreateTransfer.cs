using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vinance.Contracts.Enumerations;
using Vinance.Contracts.Interfaces;
using Vinance.Web.Models;

namespace Vinance.Web.Components.Transfer
{
    public class CreateTransfer : ViewComponent
    {
        private readonly IAccountApi _accountApi;
        private readonly ICategoryApi _categoryApi;

        public CreateTransfer(IAccountApi accountApi, ICategoryApi categoryApi)
        {
            _accountApi = accountApi;
            _categoryApi = categoryApi;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var accounts = await _accountApi.GetAll();
            var categories = await _categoryApi.GetCategories(CategoryType.Transfer);
            var model = new CreateTransferViewmodel
            {
                AccountList = accounts.Select(a => new SelectListItem(a.Name, a.Id.ToString())),
                CategoryList = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()))
            };
            return View("CreateTransfer", model);
        }
    }
}