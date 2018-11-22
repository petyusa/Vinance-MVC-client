using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Income
{
    using Contracts.Interfaces;

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