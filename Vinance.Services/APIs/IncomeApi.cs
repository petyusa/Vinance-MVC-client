using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vinance.Services.APIs
{
    using Contracts.Interfaces;
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

        public async Task<IEnumerable<Income>> GetAll()
        {
            var client = _factory.CreateClient("authenticated-client");
            var response = await client.GetAsync("incomes");
            return await _responseHandler.HandleAsync<IEnumerable<Income>>(response);
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