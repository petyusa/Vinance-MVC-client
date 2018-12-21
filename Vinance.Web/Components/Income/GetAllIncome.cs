using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Income
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;

    public class GetAllIncome : ViewComponent
    {
        private readonly IIncomeApi _incomeApi;
        private readonly ICategoryApi _categoryApi;
        private readonly IAccountApi _accountApi;

        public GetAllIncome(IIncomeApi incomeApi, ICategoryApi categoryApi, IAccountApi accountApi)
        {
            _incomeApi = incomeApi;
            _categoryApi = categoryApi;
            _accountApi = accountApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? accountId = null, int? categoryId = null, DateTime? from = null, DateTime? to = null, string order = "date_desc", int page = 1, int pageSize = 20)
        {
            if (!from.HasValue || !to.HasValue)
            {
                to = DateTime.Now;
                from = to.Value.Subtract(TimeSpan.FromDays(30));
            }

            var incomes = await _incomeApi.GetAll(accountId, categoryId, from, to, page, pageSize, order);
            var categories = await _categoryApi.GetCategories(CategoryType.Income);
            var accounts = await _accountApi.GetAll();

            incomes.CategoryId = categoryId;
            incomes.AccountId = accountId;
            incomes.From = from;
            incomes.To = to;
            incomes.Order = order;
            incomes.Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString(), categoryId.HasValue && categoryId.Value == c.Id));
            incomes.Accounts = accounts.Select(a => new SelectListItem(a.Name, a.Id.ToString(), accountId.HasValue && accountId.Value == a.Id));

            return View("GetAllIncome", incomes);
        }
    }
}