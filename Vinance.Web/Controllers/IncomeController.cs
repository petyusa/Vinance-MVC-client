using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Controllers
{
    using Components.Income;
    using Components.MainPage;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;

    [Route("incomes")]
    public class IncomeController : Controller
    {
        private readonly IIncomeApi _incomeApi;

        public IncomeController(IIncomeApi incomeApi)
        {
            _incomeApi = incomeApi;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return ViewComponent(typeof(GetAllIncome));
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return ViewComponent(typeof(CreateIncome));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Income income)
        {
            await _incomeApi.Create(income);
            return ViewComponent(typeof(MainPageTables));
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult Edit(int incomeId)
        {
            return ViewComponent(typeof(EditIncome), new { incomeId });
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(Income income)
        {
            await _incomeApi.Update(income);
            return ViewComponent(typeof(GetAllIncome));
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(int incomeId)
        {
            await _incomeApi.Delete(incomeId);
            return ViewComponent(typeof(GetAllIncome));
        }
    }
}