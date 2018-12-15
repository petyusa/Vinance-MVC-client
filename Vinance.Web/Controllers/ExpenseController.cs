using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vinance.Contracts.Enumerations;

namespace Vinance.Web.Controllers
{
    using Components.Expense;
    using Components.MainPage;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;
    using Models;

    [Route("expenses")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseApi _expenseApi;
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseApi expenseApi, IMapper mapper)
        {
            _expenseApi = expenseApi;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll(int? categoryId = null, DateTime? from = null, DateTime? to = null, string sortOrder = "date_desc", int page = 1, int pageSize = 20)
        {
            return ViewComponent(typeof(GetAllExpense), new { categoryId, from, to, sortOrder, page, pageSize });
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = await _expenseApi.Create(expense);
            if (success)
            {
                return ViewComponent(typeof(MainPageTables));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult Edit(int expenseId)
        {
            return ViewComponent(typeof(EditExpense), expenseId);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(CreateExpenseViewmodel expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Expense>(expense);
            var success = await _expenseApi.Update(model);
            if (success)
            {
                return ViewComponent(typeof(GetAllExpense));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(int expenseId)
        {
            var success = await _expenseApi.Delete(expenseId);
            if (success)
            {
                return ViewComponent(typeof(GetAllExpense));
            }

            return BadRequest();
        }
    }
}