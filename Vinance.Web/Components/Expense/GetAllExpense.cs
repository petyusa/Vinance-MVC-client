using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Expense
{
    using Contracts.Interfaces;

    public class GetAllExpense : ViewComponent
    {
        private readonly IExpenseApi _expenseApi;

        public GetAllExpense(IExpenseApi expenseApi)
        {
            _expenseApi = expenseApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(DateTime? from = null, DateTime? to = null, string order = "date_desc", int page = 1, int pageSize = 20)
        {
            var expenses = await _expenseApi.GetAll(from, to, page, pageSize, order);
            return View("GetAllExpense", expenses);
        }
    }
}