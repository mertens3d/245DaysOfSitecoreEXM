using System.Web.Mvc;

namespace LearnEXM.Foundation.Extensions.Extensions.AssetsHelper
{
  public class AssetsHelper
  {
    public static AssetsHelper GetInstance(HtmlHelper htmlHelper)
    {
      AssetsHelper toReturn = null;

      var instanceKey = "AssetsHelperInstance";

      var context = htmlHelper.ViewContext.HttpContext;
      if (context != null)
      {
         toReturn = (AssetsHelper)context.Items[instanceKey];

        if (toReturn == null)
        {
          context.Items.Add(instanceKey, toReturn = new AssetsHelper());
        }
      }

      return toReturn;
    }

    public ItemRegistrar Styles { get; private set; }
    public ItemRegistrar Scripts { get; private set; }

    public AssetsHelper()
    {
      Styles = new ItemRegistrar(ItemRegistrarFormaters.StyleFormat);
      Scripts = new ItemRegistrar(ItemRegistrarFormaters.ScriptFormat);
    }
  }
}