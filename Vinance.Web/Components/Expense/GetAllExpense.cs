﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var expenses = await _expenseApi.GetAll();
            return View("GetAllExpense", expenses);
        }
    }
}