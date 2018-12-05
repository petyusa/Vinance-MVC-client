using Microsoft.AspNetCore.Mvc;
using Vinance.Contracts.Enumerations;

namespace Vinance.Web.Components.Category
{
    using Models;

    public class CreateCategoryInTable : ViewComponent
    {
        public IViewComponentResult Invoke(CategoryType type)
        {
            var model = new CategoryViewmodel
            {
                Type = type
            };
            return View("CreateCategoryInTable", model);
        }
    }
}