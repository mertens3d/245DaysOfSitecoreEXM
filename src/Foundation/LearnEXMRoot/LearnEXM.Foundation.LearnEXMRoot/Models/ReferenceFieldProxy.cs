using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace LearnEXM.Foundation.LearnEXMRoot.Models
{
  public class ReferenceFieldProxy<T> : _baseFieldProxy where T : _baseItemProxy, new()
  {
    public T TargetItem
    {
      get
      {
        if (_targetItem == null && ReferenceField != null)
        {
          _targetItem = new T();
          _targetItem.InstantiateWith(ReferenceField.TargetItem);
        }

        return _targetItem;
      }
    }
    private ReferenceField _referenceField;
    private T _targetItem;

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