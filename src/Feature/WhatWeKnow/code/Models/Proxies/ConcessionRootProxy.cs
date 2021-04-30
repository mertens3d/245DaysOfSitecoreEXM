﻿using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data;
using System.Collections.Generic;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies
{
  public class ConcessionRootProxy : _baseItemProxy
  {
    public ConcessionRootProxy()
    {
      //for Generics only
    }

    public ConcessionRootProxy(ID itemId) : base(itemId)
    {
    }

    public List<ConcessionCategoryProxy> ConcessionCategories { get; private set; }
    protected override ID AssociatedTemplateId { get; } = ProjectConst.Items.Content.ConcesssionsFolderItemID;

    protected override void CommonCTOR()
    {
      ConcessionCategories = this.ChildrenOfTemplateType<ConcessionCategoryProxy>();
      
    }
  }
}