using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vinance.Web.Components.Account
{
    using Contracts.Interfaces;

    public class EditAccount : ViewComponent
    {
        private readonly IAccountApi _accountApi;
        private readonly IMapper _mapper;

        public EditAccount(IAccountApi accountApi, IMapper mapper)
        {
            _accountApi = accountApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int accountId)
        {
            var account = await _accountApi.GetById(accountId);
            
            return View("EditAccount", account);
        }
    }
}
