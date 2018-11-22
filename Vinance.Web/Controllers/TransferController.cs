using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Vinance.Web.Controllers
{
    using Components.Account;
    using Components.Transfer;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return ViewComponent(typeof(GetAllTransfer));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Transfer transfer)
        {
            await _transferApi.Create(transfer);
            return RedirectToAction("GetAll");
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