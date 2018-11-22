using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Income
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;
    using Models;

    public class EditIncome : ViewComponent
    {
        private readonly IAccountApi _accountApi;
        private readonly ICategoryApi _categoryApi;
        private readonly IIncomeApi _incomeApi;
        private readonly IMapper _mapper;

        public EditIncome(IAccountApi accountApi, ICategoryApi categoryApi, IIncomeApi incomeApi, IMapper mapper)
        {
            _accountApi = accountApi;
            _categoryApi = categoryApi;
            _incomeApi = incomeApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int incomeId)
        {
            var income = await _incomeApi.Get(incomeId);
            var accounts = await _accountApi.GetAll();
            var categories = await _categoryApi.GetCategories(CategoryType.Income);
            var model = _mapper.Map<CreateIncomeViewmodel>(income);
            model.AccountList = accounts.Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            model.CategoryList = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()));

            return View("EditIncome", model);
        }
    }
}