using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Controllers
{
    using Components.Category;
    using Contracts.Enumerations;
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

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            await _categoryApi.GetCategories();
            return ViewComponent(typeof(GetAllCategory));
        }

        [HttpGet]
        [Route("create")]
        public IActionResult CreateInTable(CategoryType type)
        {
            return ViewComponent(typeof(CreateCategoryInTable), new { type });
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CategoryViewmodel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Category>(category);
            var success = await _categoryApi.Create(model);
            if (success)
            {
                return ViewComponent(typeof(CategoryTables));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult EditInTable(int categoryId)
        {
            return ViewComponent(typeof(EditCategoryInTable), new { categoryId });
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(CategoryViewmodel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Category>(category);
            var success = await _categoryApi.Update(model);
            if (success)
            {
                return ViewComponent(typeof(CategoryTables));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var success = await _categoryApi.Delete(categoryId);
            if (success)
            {
                return ViewComponent(typeof(CategoryTables));
            }

            return BadRequest();
        }
    }
}