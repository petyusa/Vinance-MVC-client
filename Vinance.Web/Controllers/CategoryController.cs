using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinance.Contracts.Enumerations;
using Vinance.Contracts.Interfaces;

namespace Vinance.Web.Controllers
{
    [Route("categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryApi _categoryApi;

        public CategoryController(ICategoryApi categoryApi)
        {
            _categoryApi = categoryApi;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index(CategoryType? type)
        {
            var categories = await _categoryApi.GetCategories(type);
            return View(categories);
        }
    }
}