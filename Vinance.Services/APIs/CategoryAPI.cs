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
    using Contracts.Models;
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

        public async Task<IEnumerable<Category>> GetCategories(CategoryType? type = null, DateTime? from = null, DateTime? to = null)
        {
            if (!from.HasValue || !to.HasValue)
            {
                var date = DateTime.Now;
                from = new DateTime(date.Year, date.Month, 1);
                to = from.Value.AddMonths(1).AddDays(-1);
            }

            var query = "?";
            query += $"from={from:MM-dd-yyyy}&to={to:MM-dd-yyyy}";
            if (type != null)
            {
                query += $"&type={type}";

            }

            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync($"categories{query}");
            var categories = await _responseHandler.HandleAsync<IEnumerable<Category>>(response);

            return categories.OrderBy(c => c.Name);
        }

        public async Task<IEnumerable<CategoryStatistics>> GetStats(CategoryType type = CategoryType.Expense, DateTime? from = null, DateTime? to = null, string by = null)
        {
            if (!from.HasValue || !to.HasValue)
            {
                var date = DateTime.Now;
                from = new DateTime(date.Year, 1, 1);
                to = from.Value.AddYears(1).AddDays(-1);
            }
            var query = "?";
            query += $"from={from:MM-dd-yyyy}&to={to:MM-dd-yyyy}";
            query += $"&type={type}";

            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync($"categories/average{query}");
            var result = (await _responseHandler.HandleAsync<IEnumerable<CategoryStatistics>>(response)).ToList();

            var categoryNames = result.SelectMany(cs => cs.Items.Select(c => c.Name)).Distinct().ToList();

            foreach (var categoryStatistics in result)
            {
                foreach (var categoryName in categoryNames)
                {
                    if (categoryStatistics.Items.All(cs => cs.Name != categoryName))
                    {
                        categoryStatistics.Items = categoryStatistics.Items.Concat(new[] { new CategoryStatisticsItem { Name = categoryName, Balance = 0 } });
                    }
                }
            }

            return result;
        }

        public async Task<Category> Get(int categoryId)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync($"categories/{categoryId}");

            return await _responseHandler.HandleAsync<Category>(response);
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