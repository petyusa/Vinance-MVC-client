using System.Collections.Generic;
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

            var query = "";
            if (type != null)
            {
                query = $"?type={type}";

            }
            var response = await client.GetAsync($"categories{query}");

            return await _responseHandler.HandleAsync<IEnumerable<Category>>(response);
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