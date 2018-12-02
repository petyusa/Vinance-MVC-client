using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
            var model = _mapper.Map<Expense>(expense);
            await _expenseApi.Update(model);
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