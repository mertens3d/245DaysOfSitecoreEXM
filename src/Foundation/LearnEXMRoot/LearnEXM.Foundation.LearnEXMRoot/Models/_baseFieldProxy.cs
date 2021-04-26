using Sitecore.Data;
using Sitecore.Data.Items;

namespace LearnEXM.Foundation.LearnEXMRoot
{
  public abstract class _baseFieldProxy {

    protected Item Item;
    protected ID FieldId;

    public bool IsValidItem { get { return Item != null && !FieldId.IsNull; } }
    public _baseFieldProxy(Item item, ID fieldID)
    {
      this.Item = item;
      this.FieldId = fieldID;
    }
  }
}