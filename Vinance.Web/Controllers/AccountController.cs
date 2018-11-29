﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            Response.Cookies.Append("theme", "dark");
            return View();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Account model)
        {
            await _accountApi.Create(model);
            return RedirectToAction("GetAll");
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromForm]int accountId)
        {
            await _accountApi.Delete(accountId);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return ViewComponent(typeof(GetAllAccount));
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(Account account)
        {
            await _accountApi.Update(account);
            return RedirectToAction("GetAll");
        }
    }
}