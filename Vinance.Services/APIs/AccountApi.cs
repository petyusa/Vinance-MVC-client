using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vinance.Services.APIs
{
    using Contracts.Interfaces;
    using Contracts.Models.Domain;
    using Extensions;

    public class AccountApi : IAccountApi
    {
        private readonly IHttpClientFactory _factory;
        private readonly IResponseHandler _responseHandler;

        public AccountApi(IHttpClientFactory factory, IResponseHandler responseHandler)
        {
            _factory = factory;
            _responseHandler = responseHandler;
        }

        public async Task<Account> Create(Account account)
        {
            var client = _factory.CreateClient("authenticated-client");

            var response = await client.PostAsJsonAsync("accounts", account);

            return await _responseHandler.HandleAsync<Account>(response);
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            var client = _factory.CreateClient("authenticated-client");

            var response = await client.GetAsync("accounts");

            return await _responseHandler.HandleAsync<IEnumerable<Account>>(response);
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

            var response = await client.DeleteAsync($"accounts/{accountId}");

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception();
            }
        }
    }
}