using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApi.GetCategories();
            return View("GetAllCategory", categories);
        }
    }
}