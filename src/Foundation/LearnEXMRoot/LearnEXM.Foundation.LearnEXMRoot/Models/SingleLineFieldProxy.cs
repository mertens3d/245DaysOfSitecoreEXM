using Sitecore.Data;
using Sitecore.Data.Items;

namespace LearnEXM.Foundation.LearnEXMRoot
{
  public class SingleLineFieldProxy : _baseFieldProxy
  {
    public SingleLineFieldProxy(Item item, ID fieldName) : base(item, fieldName)
    {
    }

    public string Value
    {
      get
      {
        return Item != null ? Item[FieldId] : null;
      }
    }
  }
}