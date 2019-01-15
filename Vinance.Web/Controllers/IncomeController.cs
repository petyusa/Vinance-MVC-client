using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Vinance.Web.Controllers
{
    using Components.Income;
    using Components.MainPage;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;

    [Route("incomes")]
    public class IncomeController : Controller
    {
        private readonly IIncomeApi _incomeApi;
        private readonly IMapper _mapper;

        public IncomeController(IIncomeApi incomeApi, IMapper mapper)
        {
            _incomeApi = incomeApi;
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
        public IActionResult GetAll(int? accountId = null, int? categoryId = null, DateTime? from = null, DateTime? to = null, string order = "date_desc", int page = 1, int pageSize = 20)
        {
            return ViewComponent(typeof(GetAllIncome), new { accountId, categoryId, from, to, order, page, pageSize });
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return ViewComponent(typeof(CreateIncome));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateIncomeViewmodel income)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var model = _mapper.Map<Income>(income);
            var success = await _incomeApi.Create(model);
            if (success)
            {
                return ViewComponent(typeof(MainPageTables));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult Edit(int incomeId)
        {
            return ViewComponent(typeof(EditIncome), incomeId);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(CreateIncomeViewmodel income)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Income>(income);
            var success = await _incomeApi.Update(model);
            if (success)
            {
                return ViewComponent(typeof(GetAllIncome));
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(int incomeId)
        {
            var success = await _incomeApi.Delete(incomeId);
            if (success)
            {
                return ViewComponent(typeof(GetAllIncome));
            }

            return BadRequest();
        }
    }
}