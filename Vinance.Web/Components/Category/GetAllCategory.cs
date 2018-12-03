using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Vinance.Contracts.Enumerations;
using Vinance.Web.Models;

namespace Vinance.Web.Components.Category
{
    using Contracts.Interfaces;

    public class GetAllCategory : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;
        private readonly IMapper _mapper;

        public GetAllCategory(ICategoryApi categoryApi, IMapper mapper)
        {
            _categoryApi = categoryApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool editable, CategoryType? type = null)
        {
            var categories = await _categoryApi.GetCategories(type);
            var model = _mapper.Map<IEnumerable<CategoryViewmodel>>(categories);
            ViewBag.Editable = editable;
            return View("GetAllCategory", model);
        }
    }
}