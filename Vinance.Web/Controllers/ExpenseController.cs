using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinance.Contracts.Interfaces;

namespace Vinance.Web.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseApi _expenseApi;

        public ExpenseController(IExpenseApi expenseApi)
        {
            _expenseApi = expenseApi;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}