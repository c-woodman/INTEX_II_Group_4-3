using INTEX_II_Group_4_3.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace INTEX_II_Group_4_3.Infrastructure
{
    [HtmlTargetElement("div", Attributes="page-model")]
    public class PaginationTagHelper: TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PaginationTagHelper (IUrlHelperFactory temp)
        {
            urlHelperFactory = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public  ViewContext? ViewContext  { get; set; }
        public string? PageAction { get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix ="page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
        public PaginationInfo PageModel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

                TagBuilder result = new TagBuilder("div");

                for (int i = 1; i <= PageModel.TotalNumPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    PageUrlValues["pageNum"] = i;

                    tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                    tag.InnerHtml.Append(i.ToString());

                    result.InnerHtml.AppendHtml(tag);
                }

                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
