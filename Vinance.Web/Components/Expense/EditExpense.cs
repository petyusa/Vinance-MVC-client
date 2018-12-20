using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vinance.Web.Components.Expense
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;
    using Models;

    public class EditExpense : ViewComponent
    {
        private readonly IAccountApi _accountApi;
        private readonly ICategoryApi _categoryApi;
        private readonly IExpenseApi _expenseApi;
        private readonly IMapper _mapper;

        public EditExpense(IAccountApi accountApi, ICategoryApi categoryApi, IExpenseApi expenseApi, IMapper mapper)
        {
            _accountApi = accountApi;
            _categoryApi = categoryApi;
            _expenseApi = expenseApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int expenseId)
        {
            var accounts = await _accountApi.GetAll();
            var categories = await _categoryApi.GetCategories(CategoryType.Expense);

            var expense = await _expenseApi.Get(expenseId);
            var model = _mapper.Map<CreateExpenseViewmodel>(expense);

            model.AccountList = accounts.Where(a => a.IsActive).Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            model.CategoryList = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()));

            return View("EditExpense", model);
        }
    }
}