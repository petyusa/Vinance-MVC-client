using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Account
{
    using Contracts.Interfaces;
    using Web.Models.Account;

    public class EditAccountInTable : ViewComponent
    {
        private readonly IAccountApi _accountApi;
        private readonly IMapper _mapper;

        public EditAccountInTable(IAccountApi accountApi, IMapper mapper)
        {
            _accountApi = accountApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int accountId)
        {
            var account = await _accountApi.GetById(accountId);
            var model = _mapper.Map<CreateAccountViewmodel>(account);
            return View("EditAccountInTable", model);
        }
    }
}