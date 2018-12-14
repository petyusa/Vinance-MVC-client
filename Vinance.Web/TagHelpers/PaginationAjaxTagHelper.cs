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
            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;
            AddContent(output);
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
            
            var firstPage = $@"<a class='page-link'
                    href='{sb}page=1' 
                    data-ajax-method='GET'
                    data-ajax='true'
                    data-ajax-update='#{TargetId}'
                    data-ajax-mode='replace'><i data-feather='chevrons-left'></i></a>";
            var previousPage = $@"<a class='page-link'
                    href='{sb}page={Page - 1}' 
                    data-ajax-method='GET'
                    data-ajax='true'
                    data-ajax-update='#{TargetId}'
                    data-ajax-mode='replace'><i data-feather='chevron-left'></i></a>";
            var nextPage = $@"<a class='page-link'
                    href='{sb}page={Page + 1}' 
                    data-ajax-method='GET'
                    data-ajax='true'
                    data-ajax-update='#{TargetId}'
                    data-ajax-mode='replace'><i data-feather='chevron-right'></i></a>";
                var lastPage = $@"<a class='page-link'
                    href='{sb}page={TotalPages}' 
                    data-ajax-method='GET'
                    data-ajax='true'
                    data-ajax-update='#{TargetId}'
                    data-ajax-mode='replace'><i data-feather='chevrons-right'></i></a>";

            const string ulBegin = "<ul class='pagination justify-content-end'>";

            const string li = "<li class='page-item'>";
            const string liActive = "<li class='page-item active'>";
            const string liDisabled = "<li class='page-item disabled'>";

            const string liEnd = "</li>";

            const string ulEnd = "</ul>";

            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append(ulBegin);

            htmlBuilder.Append(Page == 1 ? liDisabled : li);
            htmlBuilder.Append(firstPage);
            htmlBuilder.Append(liEnd);

            htmlBuilder.Append(Page == 1 ? liDisabled : li);
            htmlBuilder.Append(previousPage);
            htmlBuilder.Append(liEnd);

            for (var i = Page - 2; i <= Page + 2; i++)
            {
                if (i > 0 && i <= TotalPages)
                {
                    htmlBuilder.Append(i == Page ? liActive : li);
                    htmlBuilder.Append($@"<a class='page-link'
                        href='{sb}page={i}' 
                        data-ajax-method='GET'
                        data-ajax='true'
                        data-ajax-update='#{TargetId}'
                        data-ajax-mode='replace'>{i}</a>");
                    htmlBuilder.Append(liEnd);
                }

            }

            htmlBuilder.Append(Page == TotalPages ? liDisabled : li);
            htmlBuilder.Append(nextPage);
            htmlBuilder.Append(liEnd);

            htmlBuilder.Append(Page == TotalPages ? liDisabled : li);
            htmlBuilder.Append(lastPage);
            htmlBuilder.Append(liEnd);

            htmlBuilder.Append(ulEnd);

            output.Content.SetHtmlContent(htmlBuilder.ToString());
        }
    }
}