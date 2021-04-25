using System.Web.Mvc;

namespace LearnEXM.Foundation.Extensions.Extensions.AssetsHelper
{
  public static class HtmlHelperExtensions
  {
    public static AssetsHelper Assets(this HtmlHelper htmlHelper)
    {
      return AssetsHelper.GetInstance(htmlHelper);
    }
  }
}