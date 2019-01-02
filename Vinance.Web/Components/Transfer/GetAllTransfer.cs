using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vinance.Web.Components.Transfer
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;

    public class GetAllTransfer : ViewComponent
    {
        private readonly ITransferApi _transferApi;
        private readonly IAccountApi _accountApi;

        public GetAllTransfer(ITransferApi transferApi, IAccountApi accountApi)
        {
            _transferApi = transferApi;
            _accountApi = accountApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? accountId, TransferType? transferType, int? categoryId, DateTime? from, DateTime? to, string order = "date_desc", int page = 1, int pageSize = 20)
        {
            if (!from.HasValue || !to.HasValue)
            {
                to = DateTime.Now;
                from = to.Value.Subtract(TimeSpan.FromDays(30));
            }

            var transfers = await _transferApi.GetAll(accountId, transferType, from, to, page, pageSize, order);
            IEnumerable<Account> accounts;
            switch (transferType)
            {
                    case TransferType.Transfer:
                    case TransferType.Saving:
                        accounts = await _accountApi.GetAll(AccountType.Spending);
                        accounts = accounts.Concat(await _accountApi.GetAll(AccountType.Saving));
                        break;
                    case TransferType.Debt:
                        accounts = await _accountApi.GetAll(AccountType.Debt);
                        break;
                    case TransferType.Loan:
                        accounts = await _accountApi.GetAll(AccountType.Loan);
                        break;
                case null:
                        accounts = await _accountApi.GetAll();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transferType), transferType, null);
            }

            transfers.CategoryId = categoryId;
            transfers.AccountId = accountId;
            transfers.TransferType = transferType;
            transfers.From = from;
            transfers.To = to;
            transfers.Order = order;
            transfers.Accounts = accounts.Select(a => new SelectListItem(a.Name, a.Id.ToString(), accountId.HasValue && accountId.Value == a.Id));

            return View("GetAllTransfer", transfers);
        }
    }
}