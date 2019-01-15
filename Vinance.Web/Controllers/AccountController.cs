using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Vinance.Web.Controllers
{
    using Components.Account;
    using Contracts.Enumerations;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;
    using Models.Account;

    [Route("accounts")]
    public class AccountController : Controller
    {
        private readonly IAccountApi _accountApi;
        private readonly IMapper _mapper;

        public AccountController(IAccountApi accountApi, IMapper mapper)
        {
            _accountApi = accountApi;
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
        public IActionResult GetAll(AccountType? accountType)
        {
            return ViewComponent(typeof(GetAllAccount), new { accountType });
        }

        [HttpGet]
        [Route("create")]
        public IActionResult CreateInTable()
        {
            return ViewComponent(typeof(CreateAccountInTable));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateAccountViewmodel account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Account>(account);
            var success = await _accountApi.Create(model);
            if (success)
            {
                return ViewComponent(typeof(GetAllAccount), new { editable = true });
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromForm]int accountId)
        {
            var success = await _accountApi.Delete(accountId);

            if (success)
            {
                return ViewComponent(typeof(GetAllAccount), new { editable = true });
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult EditInTable(int accountId)
        {
            return ViewComponent(typeof(EditAccountInTable), accountId);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(CreateAccountViewmodel account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Account>(account);
            var success = await _accountApi.Update(model);
            if (success)
            {
                return ViewComponent(typeof(GetAllAccount), new { editable = true });
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("daily-balance")]
        public IActionResult GetDailyBalance(int? accountId = null, DateTime? from = null, DateTime? to = null)
        {
            if (!from.HasValue || !to.HasValue)
            {
                to = DateTime.Now;
                from = to.Value.AddMonths(-1);
            }
            return ViewComponent(typeof(GetDailyBalances), new { accountId, from, to });
        }
    }
}