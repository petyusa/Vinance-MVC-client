using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace Vinance.Web.TagHelpers
{
    public class VinanceValidationTagHelper : TagHelper
    {
        public string VinanceValidationFor { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var error = ViewContext.ModelState.SingleOrDefault(x => x.Key == VinanceValidationFor).Value;

            if (error == null)
            {
                return;
            }

            var errorMessage = error.Errors.Select(x => x.ErrorMessage).First();

            output.TagName = "span";
            output.Attributes.Add("class", "invalid-feedback d-block");
            output.Content.SetContent(errorMessage);
        }
    }
}