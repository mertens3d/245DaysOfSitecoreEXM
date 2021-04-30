using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.LearnEXMRoot
{

  public class GenericItemProxy : _baseItemProxy
  {

    public GenericItemProxy(ID itemId) : base(itemId)
    {
    }

    public GenericItemProxy(Item item) : base(item)
    {
    }

    protected override ID AssociatedTemplateId { get; }
  }
}