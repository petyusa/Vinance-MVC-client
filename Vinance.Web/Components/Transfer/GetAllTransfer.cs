using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinance.Contracts.Interfaces;

namespace Vinance.Web.Components.Transfer
{
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