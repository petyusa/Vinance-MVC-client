using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vinance.Contracts.Models;

namespace Vinance.Services.APIs
{
    using Contracts;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;

    public class AccountApi : IAccountApi
    {
        private readonly IHttpClientFactory _factory;

        public AccountApi(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<Account> Create(Account account)
        {
            var client = _factory.CreateClient("authenticated-client");

            var json = JsonConvert.SerializeObject(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("accounts", content);

            return await HandleResponse<Account>(response);
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            var client = _factory.CreateClient("authenticated-client");

            var response = await client.GetAsync("accounts");

            return await HandleResponse<IEnumerable<Account>>(response);
        }

        public Task<string> GetById()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> Update()
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int accountId)
        {
            var client = _factory.CreateClient("authenticated-client");

            var request = new HttpRequestMessage(HttpMethod.Delete, $"accounts/{accountId}")
            {
                Content = new StringContent("", Encoding.UTF8, Constants.ApplicationJson)
            };
            var response = await client.SendAsync(request);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception();
            }
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