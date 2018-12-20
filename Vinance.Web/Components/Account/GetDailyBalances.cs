using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var dates = balances.SelectMany(x => x.DailyBalances.Keys).Distinct();

            var result = new Dictionary<DateTime, int>();
            foreach (var dateTime in dates)
            {
                result.Add(dateTime, 0);
                balances.ToList().ForEach(x =>
                {
                    if (x.DailyBalances.ContainsKey(dateTime))
                    {
                        result[dateTime] += x.DailyBalances[dateTime];
                    }
                });
            }

            return View("GetDailyBalances", result);
        }
    }
}