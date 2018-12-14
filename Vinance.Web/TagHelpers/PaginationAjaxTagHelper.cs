using System;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Vinance.Web.TagHelpers
{
    public class PaginationAjaxTagHelper : TagHelper
    {
        public int TotalPages { get; set; }
        public int? Pagesize { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string SortOrder { get; set; }
        public int? CategoryId { get; set; }
        public string Route { get; set; }
        public int Page { get; set; }
        public string TargetId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            AddContent(output);
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        private void AddContent(TagHelperOutput output)
        {
            var sb = new StringBuilder(Route);
            sb.Append("?");
            if (From.HasValue && To.HasValue)
            {
                sb.Append($"from={From}&to={To}");
                sb.Append("&");
            }

            if (CategoryId.HasValue)
            {
                sb.Append($"categoryId={CategoryId}");
                sb.Append("&");
            }

            if (!string.IsNullOrWhiteSpace(SortOrder))
            {
                sb.Append($"order={SortOrder}");
                sb.Append("&");
            }

            if (Pagesize.HasValue)
            {
                sb.Append($"pagesize={Pagesize}");
                sb.Append("&");
            }

            var html = $@"
            <a href='{sb}page=1' data-ajax-method='GET'
                data-ajax='true'
                data-ajax-update='#{TargetId}'
                data-ajax-mode='replace'><i data-feather='chevrons-left'></i></a>
            <a href='{sb}page={(Page == 1 ? 1 : Page - 1)}' data-ajax-method='GET'
                data-ajax='true'
                data-ajax-update='#{TargetId}'
                data-ajax-mode='replace'><i data-feather='chevron-left'></i></a>
            <a href='{sb}page={(Page == TotalPages ? TotalPages : Page + 1)}' data-ajax-method='GET'
                data-ajax='true'
                data-ajax-update='#{TargetId}'
                data-ajax-mode='replace'><i data-feather='chevron-right'></i></a>
            <a href='{sb}page={TotalPages}' data-ajax-method='GET'
                data-ajax='true'
                data-ajax-update='#{TargetId}'
                data-ajax-mode='replace'><i data-feather='chevrons-right'></i></a>";
            output.Content.SetHtmlContent(html);
        }
    }
}