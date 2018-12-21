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

        public async Task<IViewComponentResult> InvokeAsync(bool first = false, int? accountId = null, DateTime? from = null, DateTime? to = null)
        {
            var balances = await _accountApi.GetDailyBalances(accountId, from, to);

            var dates = first ?
                balances.FirstOrDefault()?.DailyBalances.Select(x => x.Key).ToList() :
                balances.SelectMany(x => x.DailyBalances.Keys).Distinct().ToList();

            var result = new Dictionary<DateTime, int>();

            if (dates != null)
            {
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
            }

            return View("GetDailyBalances", result);
        }
    }
}