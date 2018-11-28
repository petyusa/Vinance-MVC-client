using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vinance.Contracts.Enumerations;
using Vinance.Contracts.Interfaces;
using Vinance.Web.Models;

namespace Vinance.Web.Components.Category
{
    public class CategoryChart : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;
        private readonly IMapper _mapper;

        public CategoryChart(ICategoryApi categoryApi, IMapper mapper)
        {
            _categoryApi = categoryApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApi.GetCategories(CategoryType.Expense);
            var model = _mapper.Map<IEnumerable<CategoryViewmodel>>(categories);
            return View("CategoryChart", model);
        }
    }
}