﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Controllers
{
    using Components.Account;
    using Components.Expense;
    using Components.MainPage;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;

    [Route("expenses")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseApi _expenseApi;

        public ExpenseController(IExpenseApi expenseApi)
        {
            _expenseApi = expenseApi;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return ViewComponent(typeof(CreateExpense));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Expense expense)
        {
            var success = await _expenseApi.Create(expense);
            return ViewComponent(typeof(MainPageTables));
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(int expenseId)
        {
            await _expenseApi.Delete(expenseId);
            return ViewComponent(typeof(GetAllExpense));
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(Expense expense)
        {
            await _expenseApi.Update(expense);
            return ViewComponent(typeof(GetAllExpense));
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return ViewComponent(typeof(GetAllExpense));
        }
    }
}