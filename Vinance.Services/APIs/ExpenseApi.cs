using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vinance.Services.APIs
{
    using Contracts;
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
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync("expenses");
            return await _responseHandler.HandleAsync<IEnumerable<Expense>>(response);
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