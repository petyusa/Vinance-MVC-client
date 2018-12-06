using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Vinance.Contracts.Enumerations;

namespace Vinance.Web.Controllers
{
    using Components.Category;
    using Contracts.Interfaces;
    using Contracts.Models.Domain;
    using Models;

    [Route("categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryApi _categoryApi;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryApi categoryApi, IMapper mapper)
        {
            _categoryApi = categoryApi;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index(Category category)
        {
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
        [Route("create-table")]
        public IActionResult CreateInTable(CategoryType type)
        {
            return ViewComponent(typeof(CreateCategoryInTable), new{type});
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            await _categoryApi.GetCategories();
            return ViewComponent(typeof(GetAllCategory));
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(CategoryViewmodel category)
        {
            var model = _mapper.Map<Category>(category);
            await _categoryApi.Update(model);
            return ViewComponent(typeof(CategoryTables));
        }

        [HttpGet]
        [Route("edit-in-table")]
        public IActionResult EditInTable(int categoryId)
        {
            return ViewComponent(typeof(EditCategoryInTable), new {categoryId});
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