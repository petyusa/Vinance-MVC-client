using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vinance.Contracts.Interfaces;
using Vinance.Contracts.Models;
using Vinance.Contracts.Models.Identity;

namespace Vinance.Services
{
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

        public async Task<string> GetAccounts()
        { 
            var client = _factory.CreateClient("authenticated-client");
            var response = await client.GetAsync("accounts");
            return await response.Content.ReadAsStringAsync();
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