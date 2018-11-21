using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinance.Web.Components.Account;

namespace Vinance.Web.Controllers
{
    using Contracts.Interfaces;
    using Contracts.Models.Domain;

    [Route("transfers")]
    public class TransferController : Controller
    {
        private readonly ITransferApi _transferApi;

        public TransferController(ITransferApi transferApi)
        {
            _transferApi = transferApi;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            return ViewComponent(typeof(GetAllAccount));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Transfer transfer)
        {
            await _transferApi.Create(transfer);
            return ViewComponent(typeof(GetAllAccount));
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(Transfer transfer)
        {
            await _transferApi.Update(transfer);
            return RedirectToAction("GetAll");
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(int transferId)
        {
            await _transferApi.Delete(transferId);
            return RedirectToAction("GetAll");
        }
    }
}