using System.Net;
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

        public async Task<AuthToken> GetToken(LoginModel loginModel)
        {
            var client = _factory.CreateClient("not-authenticated-client");

            var response = await client.PostAsJsonAsync("users/token", loginModel);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var error = await _responseHandler.HandleWithErrorAsync<AuthToken>(response);
                return new AuthToken{Error = (string) error.Error};
            }
            var tokenResult = await _responseHandler.HandleWithErrorAsync<AuthToken>(response);

            return tokenResult.Data;
        }

        public async Task<AuthToken> GetToken(string refreshToken)
        {
            var client = _factory.CreateClient("not-authenticated-client");

            var response = await client.PostAsJsonAsync("users/token/refresh", refreshToken);
            var tokenResult = await _responseHandler.HandleWithErrorAsync<AuthToken>(response);

            return tokenResult.Data;
        }

        public async Task<TokenResult> Register(RegisterModel registerModel)
        {
            var client = _factory.CreateClient("not-authenticated-client");

            var response = await client.PostAsJsonAsync("users/register", registerModel);
            var result = await _responseHandler.HandleWithErrorAsync<TokenResult>(response);

            return result.Data ?? (result.Data = new TokenResult {Error = (string) result.Error});
        }

        public async Task<VinanceUser> GetUser()
        {
            var client = _factory.CreateClient("authenticated-client");

            var response = await client.GetAsync("users/me");
            var user = await _responseHandler.HandleWithErrorAsync<VinanceUser>(response);

            return user.Data;
        }

        public async Task<bool> ConfirmEmail(string email, string token)
        {
            var client = _factory.CreateClient("not-authenticated-client");

            var response = await client.PostAsJsonAsync("users/confirm-email", new { Email = email, Token = token });
            return response.IsSuccessStatusCode;
        }
    }
}