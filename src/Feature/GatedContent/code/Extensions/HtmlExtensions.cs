using Feature.GatedContent.Models;
using System.Web.Mvc;

namespace Feature.GatedContent.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString EEAccordianButton(this HtmlHelper helper, string titleRaw)
        {
            var html = "<div class=\"" + FormsConstants.EE.AccordianButtonClass + "\">" + titleRaw + "</div>";
            return new MvcHtmlString(html.ToString());
        }

        public static EEAccordianPanel EEAccordianPanel(this HtmlHelper helper, string titleRaw, bool initStateClosed)
        {
            var buttonBuilder = new TagBuilder("div");
            buttonBuilder.Attributes.Add("class", FormsConstants.EE.AccordianButtonClass);
            buttonBuilder.InnerHtml = titleRaw;
            helper.ViewContext.Writer.Write(buttonBuilder.ToString());

            var tagBuilder = new TagBuilder("div");
            tagBuilder.Attributes.Add("class", FormsConstants.EE.AccordianPanelClass);
            if (initStateClosed)
            {
                tagBuilder.Attributes.Add("style", "display:none");
            }
            else
            {
                tagBuilder.Attributes.Add("style", "display:block");
            }
            helper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));

            return new EEAccordianPanel(helper.ViewContext);
        }

        public static MvcHtmlString StyledBool(this HtmlHelper helper, bool value)
        {
            var html = "<span class=\"" + (value ? FormsConstants.EE.ColorTrue : FormsConstants.EE.ColorFalse) + "\">" + value.ToString() + "</span>";
            return new MvcHtmlString(html.ToString());
        }

        public static MvcHtmlString StyledSwitch(this HtmlHelper helper, bool value)
        {
            var html = "<span class=\""
              + (value ? FormsConstants.EE.ColorFalse : FormsConstants.EE.ColorTrue) 
                + "\">"
                + (value ? "On" : "Off")
                + "</span>";
            return new MvcHtmlString(html.ToString());
        }
    }
}
