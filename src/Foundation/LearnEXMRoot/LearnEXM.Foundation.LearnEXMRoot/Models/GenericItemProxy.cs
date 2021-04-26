using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.LearnEXMRoot
{
  public class GenericItemProxy : _baseItemProxy
  {
    private Item x;

    public GenericItemProxy(ID itemId) : base(itemId)
    {
    }

    public GenericItemProxy(Item item) : base(item)
    {
    }

    public List<GenericItemProxy> ChildrenOfTemplateType(ID templateNeedleID)
    {
      var toReturn = new List<GenericItemProxy>();

      if (Item != null)
      {
        toReturn = Item.GetChildren()
               .Where(x => x.TemplateID == templateNeedleID)
               .Select(x => new GenericItemProxy(x))
               .ToList();
      }

      return toReturn;
    }

    public Item GetItem()
    {
      return Item;
    }
  }
}