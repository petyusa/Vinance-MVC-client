using Microsoft.AspNetCore.Mvc;

namespace Vinance.Web.Components.Category
{
    using Contracts.Enumerations;
    using Contracts.Models.Domain;

    public class CreateCategory : ViewComponent
    {
        public IViewComponentResult Invoke(CategoryType type)
        {
            var model = new Category
            {
                Type = type
            };
            return View("CreateCategory", model);
        }
    }
}