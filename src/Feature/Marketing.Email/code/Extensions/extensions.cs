using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Marketing.Email.Extensions
{
  public static class Extensions
  {
    public static IHtmlString EETableEnd(this HtmlHelper helper)
    {
      string toReturn = string.Empty;
      if (Sitecore.Context.PageMode.IsExperienceEditor)
      {
        toReturn = "</table>";
      }
      return MvcHtmlString.Create(toReturn);
    }

    /// <summary>
    /// Draws a table wrapper so that the component displays correctly in Experience Editor
    /// </summary>
    /// <param name="helper"></param>
    /// <returns>
    /// raw html table tags
    /// </returns>
    public static IHtmlString EETableStart(this HtmlHelper helper)
    {
      string toReturn = string.Empty;
      if (Sitecore.Context.PageMode.IsExperienceEditor)
      {
        toReturn = "<table class='ee-table-fix'>";
      }
      return MvcHtmlString.Create(toReturn);
    }

    public static IHtmlString EEWarning(this HtmlHelper helper, string warningText)
    {
      string toReturn = string.Empty;
      if (Sitecore.Context.PageMode.IsExperienceEditor)
      {
        toReturn = "<div class='ee-tooltip'><span class='ee-tooltipwarning'>!</span><span class='ee-tooltiptext'>" + warningText + "</span></div>";
      }
      return MvcHtmlString.Create(toReturn);
    }
  }
}
