﻿using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using Vinance.Services.APIs;

namespace Vinance.Services
{
    using Contracts;
    using Contracts.Interfaces;
    using Services;

    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddAuthenticatedHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddHttpClient("authenticated-client", opt =>
            {
                opt.BaseAddress = new Uri(configuration["Vinance-url"]);
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ApplicationJson));
                opt.DefaultRequestHeaders.Add("Content-tpye", Constants.ApplicationJson);
            });
        }

        public static IHttpClientBuilder AddUnAuthenticatedHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddHttpClient("not-authenticated-client", opt =>
            {
                opt.BaseAddress = new Uri(configuration["Vinance-url"]);
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ApplicationJson));
                opt.DefaultRequestHeaders.Add("Content-tpye", Constants.ApplicationJson);
            });
        }

        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            services.AddSingleton(new SendGridClient(apiKey));
            return services;
        }

        public static IServiceCollection AddVinanceApis(this IServiceCollection services)
        {
            services.AddTransient<IUserApi, UserApi>();
            services.AddTransient<IAccountApi, AccountApi>();
            services.AddTransient<ICategoryApi, CategoryApi>();
            return services;
        }
    }
}