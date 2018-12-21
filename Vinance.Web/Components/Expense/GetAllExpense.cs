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
        private readonly IAccountApi _accountApi;

        public GetAllExpense(IExpenseApi expenseApi, ICategoryApi categoryApi, IAccountApi accountApi)
        {
            _expenseApi = expenseApi;
            _categoryApi = categoryApi;
            _accountApi = accountApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? accountId = null, int? categoryId = null, DateTime? from = null, DateTime? to = null, string sortOrder = "date_desc", int page = 1, int pageSize = 20)
        {
            if (!from.HasValue || !to.HasValue)
            {
                to = DateTime.Now;
                from = to.Value.Subtract(TimeSpan.FromDays(30));
            }

            var expenses = await _expenseApi.GetAll(accountId, categoryId, from, to, page, pageSize, sortOrder);
            var categories = await _categoryApi.GetCategories(CategoryType.Expense);
            var accounts = await _accountApi.GetAll();

            expenses.AccountId = accountId;
            expenses.CategoryId = categoryId;
            expenses.From = from;
            expenses.To = to;
            expenses.Order = sortOrder;
            expenses.Accounts = accounts.Select(a =>new SelectListItem(a.Name, a.Id.ToString(), accountId.HasValue && accountId.Value == a.Id));
            expenses.Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString(), categoryId.HasValue && categoryId.Value == c.Id));

            return View("GetAllExpense", expenses);
        }
    }
}