using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vinance.Services.APIs
{
    using Contracts;
    using Contracts.Enumerations;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;
    using Extensions;

    public class CategoryApi : ICategoryApi
    {
        private readonly IHttpClientFactory _factory;
        private readonly IResponseHandler _responseHandler;

        public CategoryApi(IHttpClientFactory factory, IResponseHandler responseHandler)
        {
            _factory = factory;
            _responseHandler = responseHandler;
        }

        public async Task<IEnumerable<Category>> GetCategories(CategoryType? type = null)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);

            var query = "?";
            if (type != null)
            {
                query += $"type={type}&";

            }
            var date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            query += $"from={firstDayOfMonth:MM-dd-yyyy}&to={lastDayOfMonth:MM-dd-yyyy}";
            var response = await client.GetAsync($"categories{query}");

            var categories = await _responseHandler.HandleAsync<IEnumerable<Category>>(response);
            return categories.OrderBy(c => c.Name);
        }

        public async Task<bool> Create(Category category)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.PostAsJsonAsync("categories", category);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(Category category)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.PutAsJsonAsync("categories", category);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int categoryId)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.DeleteAsync($"categories/{categoryId}");
            return response.IsSuccessStatusCode;
        }
    }
}