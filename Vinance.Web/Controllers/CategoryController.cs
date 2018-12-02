using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Vinance.Web.Controllers
{
    using Components.Category;
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
        public IActionResult Index(Category category)
        {
            Response.Cookies.Append("theme", "light");
            return View();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Category category)
        {
            var success = await _categoryApi.Create(category);
            return ViewComponent(typeof(CategoryTables));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            await _categoryApi.GetCategories();
            return ViewComponent(typeof(GetAllCategory));
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(Category category)
        {
            await _categoryApi.Update(category);
            return RedirectToAction("GetAll");
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            await _categoryApi.Delete(categoryId);
            return ViewComponent(typeof(CategoryTables));
        }
    }
}