using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Vinance.Web.Middlewares
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!_contextAccessor.HttpContext.Request.Cookies.TryGetValue("token", out var token))
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            request.Headers.Add("Authorization", "Bearer " + token);
            return base.SendAsync(request, cancellationToken);
        }
    }
}