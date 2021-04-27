using Sitecore.Data;
using Sitecore.Data.Items;

namespace LearnEXM.Foundation.LearnEXMRoot
{
  public class CheckBoxFieldProxy : _baseFieldProxy
  {
    public CheckBoxFieldProxy(Item item, ID fieldID) : base(item, fieldID)
    {
    }

    public bool Value
    {
      get
      {
        return Item != null ? Item[FieldId] == "1" : false;
      }
    }
  }
}