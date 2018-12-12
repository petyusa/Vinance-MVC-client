using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vinance.Web.Components.Category
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;

    public class CategorySelectList : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;

        public CategorySelectList(ICategoryApi categoryApi)
        {
            _categoryApi = categoryApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(CategoryType? type, int? selected)
        {
            var categories = await _categoryApi.GetCategories(type);
            var model = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString(), selected.HasValue && selected.Value == c.Id));
            return View("CategorySelectList", model);
        }
    }
}