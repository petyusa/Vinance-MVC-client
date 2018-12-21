using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Vinance.Services.APIs
{
    using Contracts;
    using Contracts.Interfaces;
    using Contracts.Models;
    using Contracts.Models.Domain;
    using Extensions;

    public class TransferApi : ITransferApi
    {
        private readonly IHttpClientFactory _factory;
        private readonly IResponseHandler _responseHandler;

        public TransferApi(IHttpClientFactory factory, IResponseHandler responseHandler)
        {
            _factory = factory;
            _responseHandler = responseHandler;
        }

        public async Task<PagedList<Transfer>> GetAll(int? accountId, DateTime? from = null, DateTime? to = null, int? page = null, int? pageSize = null, string order = null)
        {
            var sb = new StringBuilder("?");
            if (!from.HasValue || !to.HasValue)
            {
                to = DateTime.Now;
                from = to.Value.Subtract(TimeSpan.FromDays(30));
            }

            sb.Append($"from={from.Value}");
            sb.Append($"&to={to.Value}");

            if (accountId.HasValue && accountId.Value != 0)
            {
                sb.Append($"&accountId={accountId.Value}");
            }

            if (page.HasValue)
            {
                sb.Append($"&page={page.Value}");
            }

            if (pageSize.HasValue)
            {
                sb.Append($"&pagesize={pageSize.Value}");
            }

            if (!string.IsNullOrWhiteSpace(order))
            {
                sb.Append($"&order={order}");
            }

            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync($"transfers{sb}");
            return await _responseHandler.HandleAsync<PagedList<Transfer>>(response);
        }

        public async Task<Transfer> Get(int transferId)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync($"transfers/{transferId}");
            return await _responseHandler.HandleAsync<Transfer>(response);
        }

        public async Task<bool> Create(Transfer transfer)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.PostAsJsonAsync("transfers", transfer);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int transferId)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.DeleteAsync($"transfers/{transferId}");
            return response.IsSuccessStatusCode;

        }

        public async Task<bool> Update(Transfer transfer)
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.PutAsJsonAsync("transfers", transfer);
            return response.IsSuccessStatusCode;
        }
    }
}