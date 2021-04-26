using Sitecore.Data;
using Sitecore.Data.Items;
using System;

namespace LearnEXM.Foundation.LearnEXMRoot
{
  public abstract class _baseItemProxy
  {
    private Item _item;

    private ID CTORItemId { get; set; }
    public ID ItemId { get {
        return Item != null ? Item.ID : null;
      } }
    public Guid Id { get { return ItemId.Guid; } }

    protected Item Item
    {
      get
      {

        return _item ?? (_item = Sitecore.Context.Database.GetItem(CTORItemId));
      }
      set
      {
        _item = value;
      }
    }

    public _baseItemProxy(ID itemId)
    {

      this.CTORItemId = itemId;
    }

    protected _baseItemProxy(Item item)
    {
      this.Item = item;
      this.CTORItemId = item.ID;
    }

    protected _baseItemProxy(Guid itemItem)
    {
      this.CTORItemId = new ID( itemItem);
    }
  }
}