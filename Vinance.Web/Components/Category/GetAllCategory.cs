using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vinance.Web.Components.Category
{
    using Contracts.Enumerations;
    using Contracts.Interfaces;
    using Models.Category;

    public class GetAllCategory : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;
        private readonly IMapper _mapper;

        public GetAllCategory(ICategoryApi categoryApi, IMapper mapper)
        {
            _categoryApi = categoryApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool editable, CategoryType type, DateTime? from = null, DateTime? to = null)
        {
            var categories = await _categoryApi.GetCategories(type, from, to);
            var model = _mapper.Map<IEnumerable<CategoryViewmodel>>(categories);
            ViewBag.Editable = editable;
            ViewBag.Type = type;
            return View("GetAllCategory", model);
        }
    }
}