using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinance.Contracts.Interfaces;

namespace Vinance.Web.Components.Income
{
    public class GetAllIncome : ViewComponent
    {
        private readonly IIncomeApi _incomeApi;

        public GetAllIncome(IIncomeApi incomeApi)
        {
            _incomeApi = incomeApi;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var incomes = await _incomeApi.GetAll();
            return View("GetAllIncome", incomes);
        }
    }
}