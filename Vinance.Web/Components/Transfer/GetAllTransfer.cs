using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vinance.Web.Components.Transfer
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;

    public class GetAllTransfer : ViewComponent
    {
        private readonly ITransferApi _transferApi;
        private readonly ICategoryApi _categoryApi;

        public GetAllTransfer(ITransferApi transferApi, ICategoryApi categoryApi)
        {
            _transferApi = transferApi;
            _categoryApi = categoryApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId = null, DateTime? from = null, DateTime? to = null, string order = "date_desc", int page = 1, int pageSize = 20)
        {
            if (!from.HasValue || !to.HasValue)
            {
                to = DateTime.Now;
                from = to.Value.Subtract(TimeSpan.FromDays(30));
            }

            var transfers = await _transferApi.GetAll(categoryId, from, to, page, pageSize, order);
            var categories = await _categoryApi.GetCategories(CategoryType.Expense);

            transfers.CategoryId = categoryId;
            transfers.From = from;
            transfers.To = to;
            transfers.Order = order;
            transfers.Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString(), categoryId.HasValue && categoryId.Value == c.Id));

            return View("GetAllTransfer", transfers);
        }
    }
}