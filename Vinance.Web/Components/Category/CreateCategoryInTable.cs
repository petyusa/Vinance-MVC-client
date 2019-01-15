using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Category
{
    using Contracts.Enumerations;
    using Models.Category;

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