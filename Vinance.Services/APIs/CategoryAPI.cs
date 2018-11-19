using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vinance.Services.APIs
{
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

        public async Task<IEnumerable<Category>> GetCategories(CategoryType? type)
        {
            var client = _factory.CreateClient("authenticated-client");

            var query = "";
            if (type.HasValue)
            {
                query = $"?type={type}";

            }
            var response = await client.GetAsync($"categories{query}");

            return await _responseHandler.HandleAsync<IEnumerable<Category>>(response);
        }

        public async Task<bool> Create(Category category)
        {
            var client = _factory.CreateClient("authenticated-client");

            var response = await client.PostAsJsonAsync("categories", category);

            return response.IsSuccessStatusCode;
        }
    }
}