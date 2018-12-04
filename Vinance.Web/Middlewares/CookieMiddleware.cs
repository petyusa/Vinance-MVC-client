using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;

namespace Vinance.Web.Middlewares
{
    using Contracts.Models;
    using Contracts.Models.Identity;

    public static class CookieMiddleware
    {
        private static readonly HttpClient Client = new HttpClient();

        public static void UseVinanceCookie(CookieAuthenticationOptions opt, IConfiguration configuration)
        {
            opt.LoginPath = "/login";
            opt.Events = new CookieAuthenticationEvents
            {
                OnValidatePrincipal = async x =>
                {
                    var identity = (ClaimsIdentity)x.Principal.Identity;
                    var accessTokenExp = DateTimeOffset.FromUnixTimeSeconds(int.Parse(identity.FindFirst("exp").Value));

                    if (accessTokenExp < DateTimeOffset.FromUnixTimeSeconds(60))
                    {
                        var accessTokenClaim = identity.FindFirst("access_token");
                        var refreshTokenClaim = identity.FindFirst("refresh_token");

                        var refreshToken = refreshTokenClaim.Value;

                        var url = configuration["Vinance-API-url"];
                        var response = await Client.PostAsJsonAsync($"{url}users/token/refresh", new { Token = refreshToken });
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Response<AuthToken>>(json).Data;

                        identity.RemoveClaim(accessTokenClaim);
                        identity.RemoveClaim(refreshTokenClaim);

                        identity.AddClaims(new[]
                        {
                                    new Claim("access_token", result.AccessToken),
                                    new Claim("refresh_token", result.RefreshToken)
                                });

                        x.ShouldRenew = true;
                    }
                }
            };
        }
    }
}