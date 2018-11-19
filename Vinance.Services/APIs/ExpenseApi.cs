using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vinance.Services.APIs
{
    using Contracts.Interfaces;
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

        public async Task<IEnumerable<Expense>> GetAll()
        {
            var client = _factory.CreateClient("authenticated-client");

            var response = await client.GetAsync("expenses");

            return await _responseHandler.HandleAsync<IEnumerable<Expense>>(response);
        }

        public async Task<bool> Create(Expense expense)
        {
            var client = _factory.CreateClient("authenticated-client");

            var response = await client.PostAsJsonAsync("expenses", expense);

            return response.StatusCode == HttpStatusCode.Created;
        }

        public async Task Delete(int expenseId)
        {
            var client = _factory.CreateClient("authenticated-client");

            var response = await client.DeleteAsync($"expenses/{expenseId}");
        }
    }
}