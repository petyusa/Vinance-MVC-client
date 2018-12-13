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

        public GetAllIncome(IIncomeApi incomeApi, ICategoryApi categoryApi)
        {
            _incomeApi = incomeApi;
            _categoryApi = categoryApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId = null, DateTime? from = null, DateTime? to = null, string order = "date_desc", int page = 1, int pageSize = 20)
        {
            if (!from.HasValue || !to.HasValue)
            {
                to = DateTime.Now;
                from = to.Value.Subtract(TimeSpan.FromDays(30));
            }

            var incomes = await _incomeApi.GetAll(categoryId, from, to, page, pageSize, order);
            var categories = await _categoryApi.GetCategories(CategoryType.Income);

            incomes.CategoryId = categoryId;
            incomes.From = from;
            incomes.To = to;
            incomes.Order = order;
            incomes.Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString(), categoryId.HasValue && categoryId.Value == c.Id));

            return View("GetAllIncome", incomes);
        }
    }
}