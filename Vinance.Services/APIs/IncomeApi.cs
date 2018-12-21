using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Vinance.Services.APIs
{
    using Contracts.Interfaces;
    using Contracts.Models;
    using Contracts.Models.Domain;
    using Extensions;

    public class IncomeApi : IIncomeApi
    {
        private readonly IHttpClientFactory _factory;
        private readonly IResponseHandler _responseHandler;

        public IncomeApi(IHttpClientFactory factory, IResponseHandler responseHandler)
        {
            _factory = factory;
            _responseHandler = responseHandler;
        }

        public async Task<PagedList<Income>> GetAll(int? accountId, int? categoryId, DateTime? @from = null, DateTime? to = null, int? page = null, int? pageSize = null, string order = null)
        {
            var sb = new StringBuilder("?");
            if (!from.HasValue || !to.HasValue)
            {
                to = DateTime.Now;
                from = to.Value.Subtract(TimeSpan.FromDays(30));
            }

            sb.Append($"from={from.Value}");
            sb.Append($"&to={to.Value}");

            if (accountId.HasValue && accountId.Value != 0)
            {
                sb.Append($"&accountId={accountId.Value}");
            }

            if (categoryId.HasValue && categoryId.Value != 0)
            {
                sb.Append($"&categoryId={categoryId.Value}");
            }

            if (page.HasValue)
            {
                sb.Append($"&page={page.Value}");
            }

            if (pageSize.HasValue)
            {
                sb.Append($"&pagesize={pageSize.Value}");
            }

            if (!string.IsNullOrWhiteSpace(order))
            {
                sb.Append($"&order={order}");
            }

            var client = _factory.CreateClient("authenticated-client");
            var response = await client.GetAsync($"incomes{sb}");
            return await _responseHandler.HandleAsync<PagedList<Income>>(response);
        }

        public async Task<Income> Get(int incomeId)
        {
            var client = _factory.CreateClient("authenticated-client");
            var response = await client.GetAsync($"incomes/{incomeId}");
            return await _responseHandler.HandleAsync<Income>(response);
        }

        public async Task<bool> Create(Income income)
        {
            var client = _factory.CreateClient("authenticated-client");
            var response = await client.PostAsJsonAsync("incomes", income);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int incomeId)
        {
            var client = _factory.CreateClient("authenticated-client");
            var response = await client.DeleteAsync($"incomes/{incomeId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(Income income)
        {
            var client = _factory.CreateClient("authenticated-client");
            var response = await client.PutAsJsonAsync("incomes", income);
            return response.IsSuccessStatusCode;
        }
    }
}