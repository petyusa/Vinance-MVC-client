using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vinance.Services.APIs
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;
    using Contracts.Models;
    using Contracts.Models.Domain;
    using Microsoft.AspNetCore.WebUtilities;

    public class CategoryApi : ICategoryApi
    {
        private readonly IHttpClientFactory _factory;

        public CategoryApi(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Category>> GetCategories(CategoryType? type)
        {
            var client = _factory.CreateClient("authenticated-client");

            var query = "";
            if (type.HasValue)
            {
                query = QueryHelpers.AddQueryString("", "type", type.ToString()); ;
            
            }
            var response = await client.GetAsync($"categories{query}");
            
            return await HandleResponse<IEnumerable<Category>>(response);
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