using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Vinance.Web.Controllers
{
    using Components.Account;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;

    [Route("accounts")]
    public class AccountController : Controller
    {
        private readonly IAccountApi _accountApi;

        public AccountController(IAccountApi accountApi)
        {
            _accountApi = accountApi;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Account model)
        {
            await _accountApi.Create(model);
            return ViewComponent(typeof(GetAllAccount), new { editable = true });
        }

        [HttpGet]
        [Route("create-in-table")]
        public IActionResult CreateInTable()
        {
            return ViewComponent(typeof(CreateAccountInTable));
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromForm]int accountId)
        {
            await _accountApi.Delete(accountId);
            return ViewComponent(typeof(GetAllAccount), new { editable = true });
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return ViewComponent(typeof(GetAllAccount));
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult Edit(int accountId)
        {
            return ViewComponent(typeof(EditAccount), accountId);
        }

        [HttpGet]
        [Route("edit-in-table")]
        public IActionResult EditInTable(int accountId)
        {
            return ViewComponent(typeof(EditAccountInTable), accountId);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                await _accountApi.Update(account);
            }
            return ViewComponent(typeof(GetAllAccount), new { editable = true });
        }
    }
}