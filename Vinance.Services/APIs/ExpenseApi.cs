using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Vinance.Services.APIs
{
    using Contracts;
    using Contracts.Interfaces;
    using Contracts.Models;
    using Contracts.Models.Domain;
    using Extensions;

    public class ExpenseApi : IExpenseApi
    {
        private readonly IHttpClientFactory _factory;
        private readonly IResponseHandler _responseHandler;

        public ExpenseApi(IHttpClientFactory factory, IResponseHandler responseHandler)
        {
            _factory = factory;
            _responseHandler = responseHandler;
        }

        public async Task<PagedList<Expense>> GetAll(int? accountId, int? categoryId, DateTime? from = null, DateTime? to = null, int? page = null, int? pageSize = null, string order = null)
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

            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync($"expenses{sb}");
            return await _responseHandler.HandleAsync<PagedList<Expense>>(response);
        }

        public async Task<Expense> Get(int expenseId)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync($"expenses/{expenseId}");
            return await _responseHandler.HandleAsync<Expense>(response);
        }

        public async Task<bool> Create(Expense expense)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.PostAsJsonAsync("expenses", expense);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int expenseId)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.DeleteAsync($"expenses/{expenseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(Expense expense)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.PutAsJsonAsync("expenses", expense);
            return response.IsSuccessStatusCode;
        }
    }
}