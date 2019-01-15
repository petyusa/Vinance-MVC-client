using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Transfer
{
    using Contracts.Interfaces;
    using Models.Transfer;

    public class EditTransfer : ViewComponent
    {
        private readonly IAccountApi _accountApi;
        private readonly ITransferApi _transferApi;
        private readonly IMapper _mapper;

        public EditTransfer(IAccountApi accountApi, ITransferApi transferApi, IMapper mapper)
        {
            _accountApi = accountApi;
            _transferApi = transferApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int transferId)
        {
            var accounts = await _accountApi.GetAll();

            var transfer = await _transferApi.Get(transferId);
            var model = _mapper.Map<CreateTransferViewmodel>(transfer);

            model.AccountList = accounts.Where(a => a.IsActive).Select(a => new SelectListItem(a.Name, a.Id.ToString()));

            return View("EditTransfer", model);
        }
    }
}