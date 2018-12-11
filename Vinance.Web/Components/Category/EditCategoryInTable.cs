using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Category
{
    using Contracts.Interfaces;
    using Models;

    public class EditCategoryInTable : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;
        private readonly IMapper _mapper;

        public EditCategoryInTable(ICategoryApi categoryApi, IMapper mapper)
        {
            _categoryApi = categoryApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var category = await _categoryApi.Get(categoryId);
            var model = _mapper.Map<CategoryViewmodel>(category);
            return View("EditCategoryInTable", model);
        }
    }
}