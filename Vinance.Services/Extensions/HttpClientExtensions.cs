using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vinance.Services.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string requestUri, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PostAsync(requestUri, content);
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient client, string requestUri, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var request = new HttpRequestMessage(HttpMethod.Put, requestUri)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            return await client.SendAsync(request);
        }
    }
}