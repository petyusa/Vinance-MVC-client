using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Account
{
    using Contracts.Interfaces;

    public class GetDailyBalances : ViewComponent
    {
        private readonly IAccountApi _accountApi;

        public GetDailyBalances(IAccountApi accountApi)
        {
            _accountApi = accountApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(DateTime? from = null, DateTime? to = null)
        {
            var balances = await _accountApi.GetDailyBalances(from, to);
            return View("GetDailyBalances", balances);
        }
    }
}