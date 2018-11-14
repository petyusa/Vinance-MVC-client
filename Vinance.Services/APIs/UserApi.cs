using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vinance.Services.APIs
{
    using Contracts.Interfaces;
    using Contracts.Models;
    using Contracts.Models.Identity;

    public class UserApi : IUserApi
    {
        private readonly IHttpClientFactory _factory;

        public UserApi(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<TokenResult> GetToken(LoginModel loginModel)
        {
            var client = _factory.CreateClient("unauthenticated-client");
            
            var json = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("users/token", content);
            var tokenResult = await HandleResponse<TokenResult>(response);

            return tokenResult;
        }

        public async Task<VinanceUser> Register(RegisterModel registerModel)
        {
            var client = _factory.CreateClient("unauthenticated-client");

            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("users/register", content);
            var user = await HandleResponse<VinanceUser>(response);

            return user;
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