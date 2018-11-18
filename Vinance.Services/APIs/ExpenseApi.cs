using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vinance.Contracts.Interfaces;
using Vinance.Contracts.Models;
using Vinance.Contracts.Models.Domain;

namespace Vinance.Services.APIs
{
    public class ExpenseApi : IExpenseApi
    {
        private readonly IHttpClientFactory _factory;

        public ExpenseApi(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Expense>> GetAll()
        {
            var client = _factory.CreateClient("authenticated-client");

            var response = await client.GetAsync("expenses");

            return await HandleResponse<IEnumerable<Expense>>(response);
        }

        public Task<bool> Create(Expense expense)
        {
            throw new System.NotImplementedException();
        }

        private static async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response<T>>(json);

            if (result.Error != null)
            {
                throw new Exception(result.Error.ToString());
            }

            return result.Data;
        }
    }
}