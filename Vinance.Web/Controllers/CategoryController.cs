using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Controllers
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;

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

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Category category)
        {
            var success = await _categoryApi.Create(category);
            var message = new { success };
            return Json(message);
        }
    }
}