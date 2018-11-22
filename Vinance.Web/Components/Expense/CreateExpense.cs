using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Expense
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;
    using Models;

    public class CreateExpense : ViewComponent
    {
        private readonly IAccountApi _accountApi;
        private readonly ICategoryApi _categoryApi;

        public CreateExpense(IAccountApi accountApi, ICategoryApi categoryApi)
        {
            _accountApi = accountApi;
            _categoryApi = categoryApi;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var accounts = await _accountApi.GetAll();
            var categories = await _categoryApi.GetCategories(CategoryType.Expense);
            var model = new CreateExpenseViewmodel
            {
                AccountList = accounts.Select(a => new SelectListItem(a.Name, a.Id.ToString())),
                CategoryList = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()))
            };

            return View("CreateExpense", model);
        }
    }
}