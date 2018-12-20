using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Text;

namespace Vinance.Web.TagHelpers
{
    public class PaginationAjaxTagHelper : AnchorTagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        public PaginationAjaxTagHelper(IHtmlGenerator generator, IUrlHelperFactory urlHelperFactory) : base(generator)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public int TotalPages { get; set; }
        public int? Pagesize { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string SortOrder { get; set; }
        public int? CategoryId { get; set; }
        public int PageNumber { get; set; }
        public string TargetId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;
            AddContent(output);
        }

        private void AddContent(TagHelperOutput output)
        {
            var rootValues = new RouteValueDictionary
            {
                {"from", From},
                {"to", To},
                {"categoryId", CategoryId},
                {"sortorder", SortOrder},
                {"pagesize", Pagesize}
            };

            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            var rootUrl = urlHelper.Action(Action, Controller, rootValues);
            
            var firstPage = $@"<a class='page-link'
                    href='{rootUrl}&page=1' 
                    data-ajax-method='GET'
                    data-ajax='true'
                    data-ajax-update='#{TargetId}'
                    data-ajax-mode='replace'><i class='fas fa-angle-double-left'></i></a>";
            var previousPage = $@"<a class='page-link'
                    href='{rootUrl}&page={PageNumber - 1}' 
                    data-ajax-method='GET'
                    data-ajax='true'
                    data-ajax-update='#{TargetId}'
                    data-ajax-mode='replace'><i class='fas fa-angle-left'></i></a>";
            var nextPage = $@"<a class='page-link'
                    href='{rootUrl}&page={PageNumber + 1}' 
                    data-ajax-method='GET'
                    data-ajax='true'
                    data-ajax-update='#{TargetId}'
                    data-ajax-mode='replace'><i class='fas fa-angle-right'></i></a>";
            var lastPage = $@"<a class='page-link'
                    href='{rootUrl}&page={TotalPages}' 
                    data-ajax-method='GET'
                    data-ajax='true'
                    data-ajax-update='#{TargetId}'
                    data-ajax-mode='replace'><i class='fas fa-angle-double-right'></i></a>";

            const string ulBegin = "<ul class='pagination justify-content-end'>";

            const string li = "<li class='page-item'>";
            const string liActive = "<li class='page-item active'>";
            const string liDisabled = "<li class='page-item disabled'>";

            const string liEnd = "</li>";

            const string ulEnd = "</ul>";

            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append(ulBegin);

            htmlBuilder.Append(PageNumber == 1 ? liDisabled : li);
            htmlBuilder.Append(firstPage);
            htmlBuilder.Append(liEnd);

            htmlBuilder.Append(PageNumber == 1 ? liDisabled : li);
            htmlBuilder.Append(previousPage);
            htmlBuilder.Append(liEnd);

            for (var i = PageNumber - 2; i <= PageNumber + 2; i++)
            {
                if (i > 0 && i <= TotalPages)
                {
                    htmlBuilder.Append(i == PageNumber ? liActive : li);
                    htmlBuilder.Append($@"<a class='page-link'
                        href='{rootUrl}&page={i}' 
                        data-ajax-method='GET'
                        data-ajax='true'
                        data-ajax-update='#{TargetId}'
                        data-ajax-mode='replace'>{i}</a>");
                    htmlBuilder.Append(liEnd);
                }

            }

            htmlBuilder.Append(PageNumber == TotalPages ? liDisabled : li);
            htmlBuilder.Append(nextPage);
            htmlBuilder.Append(liEnd);

            htmlBuilder.Append(PageNumber == TotalPages ? liDisabled : li);
            htmlBuilder.Append(lastPage);
            htmlBuilder.Append(liEnd);

            htmlBuilder.Append(ulEnd);

            output.Content.SetHtmlContent(htmlBuilder.ToString());
        }
    }
}