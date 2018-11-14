using System.Net.Http;
using System.Threading.Tasks;

namespace Vinance.Services
{
    using Contracts.Interfaces;

    public class VinanceApi
    {
        private readonly IHttpClientFactory _factory;

        public VinanceApi(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<HttpResponseMessage> GetAccounts()
        {
            using (var client = _factory.CreateClient())
            {
                var response = await client.GetAsync("accounts");
                return response;
            }
        }
    }
}