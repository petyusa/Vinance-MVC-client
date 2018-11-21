using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vinance.Web.Components.Income
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;
    using Models;

    public class CreateIncome : ViewComponent
    {
        private readonly IAccountApi _accountApi;
        private readonly ICategoryApi _categoryApi;

        public CreateIncome(IAccountApi accountApi, ICategoryApi categoryApi)
        {
            _accountApi = accountApi;
            _categoryApi = categoryApi;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var accounts = await _accountApi.GetAll();
            var categories = await _categoryApi.GetCategories(CategoryType.Income);
            var model = new CreateIncomeViewmodel
            {
                AccountList = accounts.Select(a => new SelectListItem(a.Name, a.Id.ToString())),
                CategoryList = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()))
            };
            return View("CreateIncome", model);
        }
    }
}