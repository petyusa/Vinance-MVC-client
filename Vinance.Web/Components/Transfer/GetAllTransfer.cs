﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Transfer
{
    using Contracts.Interfaces;

    public class GetAllTransfer : ViewComponent
    {
        private readonly ITransferApi _transferApi;

        public GetAllTransfer(ITransferApi transferApi)
        {
            _transferApi = transferApi;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var transfers = await _transferApi.GetAll();
            return View("GetAllTransfer", transfers);
        }
    }
}