using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vinance.Contracts.Enumerations;

namespace Vinance.Web.Controllers
{
    using Components.MainPage;
    using Components.Transfer;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;
    using Models;

    [Route("transfers")]
    public class TransferController : Controller
    {
        private readonly ITransferApi _transferApi;
        private readonly IMapper _mapper;

        public TransferController(ITransferApi transferApi, IMapper mapper)
        {
            _transferApi = transferApi;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll(int? accountId, TransferType? transferType, int categoryId, DateTime? from = null, DateTime? to = null, string order = "date_desc", int page = 1, int pageSize = 20)
        {
            return ViewComponent(typeof(GetAllTransfer), new { accountId, transferType, categoryId, from, to, order, page, pageSize });
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return ViewComponent(typeof(CreateTransfer));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateTransferViewmodel transfer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Transfer>(transfer);
            var success = await _transferApi.Create(model);
            if (success)
            {
                return ViewComponent(typeof(MainPageTables));
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult Edit(int transferId)
        {
            return ViewComponent(typeof(EditTransfer), transferId);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(CreateTransferViewmodel transfer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Transfer>(transfer);
            var success = await _transferApi.Update(model);
            if (success)
            {
                return ViewComponent(typeof(GetAllTransfer));
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(int transferId)
        {
            var success = await _transferApi.Delete(transferId);
            if (success)
            {
                return ViewComponent(typeof(GetAllTransfer));
            }

            return BadRequest();
        }
    }
}