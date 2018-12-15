using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinance.Contracts.Interfaces;

namespace Vinance.Web.Components.Category
{
    public class CategoryStats : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;

        public CategoryStats(ICategoryApi categoryApi)
        {
            _categoryApi = categoryApi;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var stats = await _categoryApi.GetStats();
            return View("CategoryStats", stats);
        }
    }
}