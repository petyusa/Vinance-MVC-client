using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vinance.Web.Components.Expense
{
    using Contracts.Interfaces;
    using Contracts.Enumerations;   

    public class GetAllExpense : ViewComponent
    {
        private readonly IExpenseApi _expenseApi;
        private readonly ICategoryApi _categoryApi;

        public GetAllExpense(IExpenseApi expenseApi, ICategoryApi categoryApi)
        {
            _expenseApi = expenseApi;
            _categoryApi = categoryApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId = null, DateTime? from = null, DateTime? to = null, string order = "date_desc", int page = 1, int pageSize = 20)
        {
            if (!from.HasValue || !to.HasValue)
            {
                to = DateTime.Now;
                from = to.Value.Subtract(TimeSpan.FromDays(30));
            }

            var expenses = await _expenseApi.GetAll(categoryId, from, to, page, pageSize, order);
            var categories = await _categoryApi.GetCategories(CategoryType.Expense);

            expenses.CategoryId = categoryId;
            expenses.From = from;
            expenses.To = to;
            expenses.Order = order;
            expenses.Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString(), categoryId.HasValue && categoryId.Value == c.Id));

            return View("GetAllExpense", expenses);
        }
    }
}