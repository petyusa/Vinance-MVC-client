using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vinance.Web.Helpers;

namespace Vinance.Web.Components.Expense
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;

    public class GetAllExpense : ViewComponent
    {
        private readonly IExpenseApi _expenseApi;

        public GetAllExpense(IExpenseApi expenseApi)
        {
            _expenseApi = expenseApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId = null, DateTime? from = null, DateTime? to = null, string order = "date_desc", int page = 1, int pageSize = 20)
        {
            if (!from.HasValue || !to.HasValue)
            {
                to = DateTime.Now;
                from = to.Value.Subtract(TimeSpan.FromDays(30));
            }

            var filters = new Filters
            {
                CategoryId = categoryId,
                From = from,
                To = to,
                Order = order
            };
            ViewBag.Filters = filters;
            var expenses = await _expenseApi.GetAll(categoryId, from, to, page, pageSize, order);
            return View("GetAllExpense", expenses);
        }
    }
}