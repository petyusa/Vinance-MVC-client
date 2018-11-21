using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vinance.Services.APIs
{
    using Contracts;
    using Contracts.Interfaces;
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

        public async Task<IEnumerable<Transfer>> GetAll()
        {
            var client = _factory.CreateClient(Constants.AuthenticatedClient);
            var response = await client.GetAsync("transfers");
            return await _responseHandler.HandleAsync<IEnumerable<Transfer>>(response);
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