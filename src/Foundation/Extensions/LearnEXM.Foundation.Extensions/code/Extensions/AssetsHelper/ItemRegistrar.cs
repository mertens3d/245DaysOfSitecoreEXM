using System.Collections.Generic;
using System.Text;
using System.Web;

namespace LearnEXM.Foundation.Extensions.Extensions.AssetsHelper
{
  public class ItemRegistrar
  {
    private readonly string _format;
    private readonly IList<string> _items;

    public ItemRegistrar(string format)
    {
      _format = format;
      _items = new List<string>();
    }

    public ItemRegistrar Add(string url)
    {
      if (!_items.Contains(url))
      {
        _items.Add(url);
      }

      return this;
    }

    public IHtmlString Render()
    {
      var sb = new StringBuilder();

      foreach (var item in _items)
      {
        var fmt = string.Format(_format, item);
        sb.AppendLine(fmt);
      }

      return new HtmlString(sb.ToString());
    }
  }
}
