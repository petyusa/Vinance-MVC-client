using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Vinance.Services.Services
{
    using Contracts.Interfaces;
    using Contracts.Models;

    public class ResponseHandler : IResponseHandler
    {
        private readonly ILogger<ResponseHandler> _logger;

        public ResponseHandler(ILogger<ResponseHandler> logger)
        {
            _logger = logger;
        }

        public async Task<T> HandleAsync<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response<T>>(json);
            return result.Data;
        }

        public async Task<T> HandleWithErrorAsync<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response<T>>(json);
            return result.Data;
        }
    }
}