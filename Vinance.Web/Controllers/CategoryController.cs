using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinance.Web.Components.Account;

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

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Category category)
        {
            var success = await _categoryApi.Create(category);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            await _categoryApi.GetCategories();
            return ViewComponent(typeof(GetAllAccount));
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(Category category)
        {
            await _categoryApi.Update(category);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            await _categoryApi.Delete(categoryId);
            return RedirectToAction("GetAll");
        }
    }
}