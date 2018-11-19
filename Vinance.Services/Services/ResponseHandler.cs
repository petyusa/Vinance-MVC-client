using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vinance.Services.Services
{
    using Contracts.Interfaces;
    using Contracts.Models;

    public class ResponseHandler : IResponseHandler
    {
        public async Task<T> HandleAsync<T>(HttpResponseMessage response)
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