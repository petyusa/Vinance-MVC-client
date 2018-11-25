using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vinance.Contracts.Enumerations;

namespace Vinance.Web.Components.Category
{
    using Contracts.Interfaces;

    public class GetAllCategory : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;

        public GetAllCategory(ICategoryApi categoryApi)
        {
            _categoryApi = categoryApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(CategoryType? type = null)
        {
            var categories = await _categoryApi.GetCategories(type);
            return View("GetAllCategory", categories);
        }
    }
}