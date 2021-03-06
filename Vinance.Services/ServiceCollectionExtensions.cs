﻿using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;

namespace Vinance.Services
{
    using APIs;
    using Contracts;
    using Contracts.Interfaces;
    using Services;

    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddAuthenticatedHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddHttpClient(Constants.AuthenticatedClient, opt =>
            {
                opt.BaseAddress = new Uri(configuration["Vinance-API-url"]);
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ApplicationJson));
            });
        }

        public static IHttpClientBuilder AddNotAuthenticatedHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddHttpClient(Constants.NotAuthenticatedClient, opt =>
            {
                opt.BaseAddress = new Uri(configuration["Vinance-API-url"]);
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ApplicationJson));
            });
        }

        public static IServiceCollection AddEmailSender(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            var apiKey = configuration["SENDGRID_API_KEY"];
            services.AddSingleton(new SendGridClient(apiKey));
            return services;
        }

        public static IServiceCollection AddVinanceApis(this IServiceCollection services)
        {
            services.AddTransient<IResponseHandler, ResponseHandler>();
            services.AddTransient<IUserApi, UserApi>();
            services.AddTransient<IAccountApi, AccountApi>();
            services.AddTransient<ICategoryApi, CategoryApi>();
            services.AddTransient<IExpenseApi, ExpenseApi>();
            services.AddTransient<IIncomeApi, IncomeApi>();
            services.AddTransient<ITransferApi, TransferApi>();
            return services;
        }
    }
}