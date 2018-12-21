using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Transfer
{
    using Contracts.Interfaces;

    public class GetAllTransfer : ViewComponent
    {
        private readonly ITransferApi _transferApi;
        private readonly IAccountApi _accountApi;

        public GetAllTransfer(ITransferApi transferApi, IAccountApi accountApi)
        {
            _transferApi = transferApi;
            _accountApi = accountApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? accountId = null, int? categoryId = null, DateTime? from = null, DateTime? to = null, string order = "date_desc", int page = 1, int pageSize = 20)
        {
            if (!from.HasValue || !to.HasValue)
            {
                to = DateTime.Now;
                from = to.Value.Subtract(TimeSpan.FromDays(30));
            }

            var transfers = await _transferApi.GetAll(accountId, from, to, page, pageSize, order);
            var accounts = await _accountApi.GetAll();

            transfers.CategoryId = categoryId;
            transfers.AccountId = accountId;
            transfers.From = from;
            transfers.To = to;
            transfers.Order = order;
            transfers.Accounts = accounts.Select(a => new SelectListItem(a.Name, a.Id.ToString(), accountId.HasValue && accountId.Value == a.Id));

            return View("GetAllTransfer", transfers);
        }
    }
}