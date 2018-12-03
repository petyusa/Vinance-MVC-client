using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Category
{
    using Contracts.Interfaces;

    public class EditCategory : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;

        public EditCategory(ICategoryApi categoryApi)
        {
            _categoryApi = categoryApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var category = await _categoryApi.Get(categoryId);
            return View("EditCategory2", category);
        }
    }
}