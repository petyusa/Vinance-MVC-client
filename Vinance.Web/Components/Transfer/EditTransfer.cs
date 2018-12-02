using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vinance.Contracts.Enumerations;
using Vinance.Contracts.Interfaces;
using Vinance.Web.Models;

namespace Vinance.Web.Components.Transfer
{
    public class EditTransfer : ViewComponent
    {
        private readonly IAccountApi _accountApi;
        private readonly ICategoryApi _categoryApi;
        private readonly ITransferApi _transferApi;
        private readonly IMapper _mapper;

        public EditTransfer(IAccountApi accountApi, ICategoryApi categoryApi, ITransferApi transferApi, IMapper mapper)
        {
            _accountApi = accountApi;
            _categoryApi = categoryApi;
            _transferApi = transferApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int transferId)
        {
            var accounts = await _accountApi.GetAll();
            var categories = await _categoryApi.GetCategories(CategoryType.Transfer);

            var transfer = await _transferApi.Get(transferId);
            var model = _mapper.Map<CreateTransferViewmodel>(transfer);

            model.AccountList = accounts.Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            model.CategoryList = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()));

            return View("EditTransfer", model);
        }
    }
}