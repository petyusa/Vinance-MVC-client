using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinance.Web.Components.Account;

namespace Vinance.Web.Controllers
{
    using Components.Income;
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
        public IActionResult GetAll()
        {
            return ViewComponent(typeof(GetAllIncome));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Income income)
        {
            await _incomeApi.Create(income);
            return ViewComponent(typeof(GetAllAccount));
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(Income income)
        {
            await _incomeApi.Update(income);
            return ViewComponent(typeof(GetAllAccount));
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(int incomeId)
        {
            await _incomeApi.Delete(incomeId);
            return ViewComponent(typeof(GetAllAccount));
        }
    }
}