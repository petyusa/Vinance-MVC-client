using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using Vinance.Contracts.Interfaces;
using Vinance.Services.Services;

namespace Vinance.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddAuthenticatedHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddHttpClient("authenticated-client", opt =>
            {
                opt.BaseAddress = new Uri(configuration["Vinance-url"]);
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }

        public static IHttpClientBuilder AddUnAuthenticatedHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddHttpClient("unauthenticated-client", opt =>
            {
                opt.BaseAddress = new Uri(configuration["Vinance-url"]);
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }

        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            services.AddSingleton(new SendGridClient(apiKey));
            return services;
        }
    }
}