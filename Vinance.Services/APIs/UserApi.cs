using System.Net.Http;
using System.Threading.Tasks;

namespace Vinance.Services.APIs
{
    using Contracts.Interfaces;
    using Contracts.Models;
    using Contracts.Models.Identity;
    using Extensions;

    public class UserApi : IUserApi
    {
        private readonly IHttpClientFactory _factory;
        private readonly IResponseHandler _responseHandler;

        public UserApi(IHttpClientFactory factory, IResponseHandler responseHandler)
        {
            _factory = factory;
            _responseHandler = responseHandler;
        }

        public async Task<TokenResult> GetToken(LoginModel loginModel)
        {
            var client = _factory.CreateClient("not-authenticated-client");

            var response = await client.PostAsJsonAsync("users/token", loginModel);
            var tokenResult = await _responseHandler.HandleWithErrorAsync<TokenResult>(response);

            return tokenResult;
        }

        public async Task<TokenResult> Register(RegisterModel registerModel)
        {
            var client = _factory.CreateClient("not-authenticated-client");

            var response = await client.PostAsJsonAsync("users/register", registerModel);
            var user = await _responseHandler.HandleWithErrorAsync<TokenResult>(response);

            return user;
        }

        public async Task<VinanceUser> GetUser()
        {
            var client = _factory.CreateClient("authenticated-client");

            var response = await client.GetAsync("users/me");
            var user = await _responseHandler.HandleWithErrorAsync<VinanceUser>(response);

            return user;
        }
    }
}