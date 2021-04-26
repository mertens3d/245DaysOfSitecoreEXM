using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace LearnEXM.Foundation.LearnEXMRoot.Models
{
  public class ReferenceFieldProxy : _baseFieldProxy
  {
    public GenericItemProxy TargetItem { get { return ReferenceField != null ? new GenericItemProxy(ReferenceField.TargetItem) : null; } }
    private ReferenceField _referenceField;
    public ReferenceField ReferenceField
    {
      get
      {
        return _referenceField ?? (_referenceField = Item != null ?
              (ReferenceField)Item.Fields[FieldId]
              : null
              );
      }
    }
    public ReferenceFieldProxy(Item item, ID fieldID) : base(item, fieldID)
    {
    }
  }
}