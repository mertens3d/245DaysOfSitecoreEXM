using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.LearnEXMRoot
{
  public abstract class _baseItemProxy
  {
    private Item _item;

    public _baseItemProxy()
    {
      // for generics instantiation only
    }

    public _baseItemProxy(ID itemId)
    {
      this.CTORItemId = itemId;
      CommonCTOR();
    }

    protected _baseItemProxy(Item item)
    {
      this.Item = item;
      this.CTORItemId = item.ID;

      CommonCTOR();
    }

    protected _baseItemProxy(Guid itemItem)
    {
      this.CTORItemId = new ID(itemItem);
      CommonCTOR();
    }

    public Guid Id { get { return ItemId.Guid; } }
    public ID ItemId
    {
      get
      {
        return Item != null ? Item.ID : null;
      }
    }

    protected abstract ID AssociatedTemplateId { get; }
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

    private ID CTORItemId { get; set; }

    public List<T> ChildrenOfTemplateType<T>() where T : _baseItemProxy, new()
    {
      var toReturn = new List<T>();

      if (Item != null)
      {
        toReturn = Item.GetChildren()
               .Where(x => x.TemplateID == AssociatedTemplateId)
               .Select(x =>
               {
                 var newObj = new T();
                 newObj.InstantiateWith(x);
                 return newObj;
               })
               .ToList();
      }

      return toReturn;
    }

    //public List<GenericItemProxy> GenericChildrenOfTemplateType(ID templateNeedleID)
    //{
    //  var toReturn = new List<GenericItemProxy>();

    //  if (Item != null)
    //  {
    //    toReturn = Item.GetChildren()
    //           .Where(x => x.TemplateID == templateNeedleID)
    //           .Select(x => new GenericItemProxy(x))
    //           .ToList();
    //  }

    //  return toReturn;
    //}

    public Item GetItem()
    {
      return Item;
    }

    protected virtual void CommonCTOR() { }
    public void InstantiateWith(Item item)
    {
      this.Item = item;
      this.CTORItemId = item.ID;
      CommonCTOR();
    }
  }
}