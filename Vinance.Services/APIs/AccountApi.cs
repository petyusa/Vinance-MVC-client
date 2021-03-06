﻿using System;
using System.Collections.Generic;
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

    public class AccountApi : IAccountApi
    {
        private readonly IHttpClientFactory _factory;
        private readonly IResponseHandler _responseHandler;

        public AccountApi(IHttpClientFactory factory, IResponseHandler responseHandler)
        {
            _factory = factory;
            _responseHandler = responseHandler;
        }

        public async Task<bool> Create(Account account)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.PostAsJsonAsync("accounts", account);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Account>> GetAll(AccountType? accountType = null)
        {
            var query = "";
            if (accountType.HasValue)
            {
                query = $"?accounttype={accountType.Value}";
            }

            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync($"accounts{query}");
            return await _responseHandler.HandleAsync<IEnumerable<Account>>(response);
        }

        public async Task<Account> GetById(int accountId)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync($"accounts/{accountId}");
            return await _responseHandler.HandleAsync<Account>(response);
        }

        public async Task<bool> Update(Account account)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.PutAsJsonAsync("accounts", account);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int accountId)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.DeleteAsync($"accounts/{accountId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<DailyBalanceList>> GetDailyBalances(int? accountId = null, DateTime? from = null, DateTime? to = null)
        {
            var query = "?";

            if (accountId.HasValue)
            {
                query += $"accountId={accountId.Value}&";
            }

            if (!from.HasValue || !to.HasValue)
            {
                var date = DateTime.Now;
                from = new DateTime(date.Year, date.Month, 1);
                to = from.Value.AddMonths(1).AddDays(-1);
            }

            query += $"from={from:MM-dd-yyyy}&to={to:MM-dd-yyyy}";

            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync($"accounts/daily-balances{query}");
            return await _responseHandler.HandleAsync<List<DailyBalanceList>>(response);
        }
    }
}