﻿using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Vinance.Web.Middlewares
{
    using Contracts.Models;
    using Contracts.Models.Identity;

    public static class CookieMiddleware
    {
        private static readonly HttpClient Client = new HttpClient();

        public static void RefreshTokenHandler(CookieAuthenticationOptions opt, IConfiguration configuration)
        {
            opt.LoginPath = "/login";
            opt.Events = new CookieAuthenticationEvents
            {
                OnValidatePrincipal = async context =>
                {
                    var identity = (ClaimsIdentity)context.Principal.Identity;
                    var expClaim = identity.FindFirst("exp");
                    var accessTokenExp = DateTimeOffset.FromUnixTimeSeconds(int.Parse(expClaim.Value));

                    if (accessTokenExp < DateTime.UtcNow.AddMinutes(1))
                    {
                        var accessTokenClaim = identity.FindFirst("access_token");
                        var refreshTokenClaim = identity.FindFirst("refresh_token");

                        var refreshToken = refreshTokenClaim.Value;

                        var url = configuration["Vinance-API-url"];
                        var response = await Client.PostAsJsonAsync($"{url}users/token/refresh", new { Token = refreshToken });
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            context.RejectPrincipal();
                            return;
                        }
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Response<AuthToken>>(json).Data;

                        identity.RemoveClaim(expClaim);
                        identity.RemoveClaim(accessTokenClaim);
                        identity.RemoveClaim(refreshTokenClaim);

                        identity.AddClaims(new[]
                        {
                            new Claim("exp", ((DateTimeOffset)result.Expires).ToUnixTimeSeconds().ToString()), 
                            new Claim("access_token", result.AccessToken),
                            new Claim("refresh_token", result.RefreshToken)
                        });

                        context.ShouldRenew = true;
                    }
                }
            };
        }
    }
}