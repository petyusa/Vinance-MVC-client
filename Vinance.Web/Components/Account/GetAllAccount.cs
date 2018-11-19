﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Account
{
    using Contracts.Interfaces;

    public class GetAllAccount : ViewComponent
    {
        private readonly IAccountApi _accountApi;

        public GetAllAccount(IAccountApi accountApi)
        {
            _accountApi = accountApi;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var accounts = await _accountApi.GetAll();
            return View("GetAllAccount", accounts);
        }
    }
}