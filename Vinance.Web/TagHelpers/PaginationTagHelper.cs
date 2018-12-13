using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Vinance.Web.TagHelpers
{
    public class PaginationTagHelper : TagHelper
    {
        public int TotalPageNumber { get; set; }
        public int SelectedPage { get; set; }
        public string Route { get; set; }
        public int? Pagesize { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            AddContent(output);
        }

        private void AddContent(TagHelperOutput output)
        {
            var sb = new StringBuilder(Route);
            if (!Pagesize.HasValue)
            {
                Pagesize = 20;
            }
            var html = $"<a href='{Route}?page=1&pagesize={Pagesize}'>HAHA<a/>";
            output.Content.SetHtmlContent(html);
        }
    }
}