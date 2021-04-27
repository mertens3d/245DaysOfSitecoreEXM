using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data;
using System.Collections.Generic;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies
{
  public class ConcessionItemsProxy : _baseItemProxy
  {
    public ConcessionItemsProxy()
    {
      //for Generics only
    }

    public ConcessionItemsProxy(ID itemId) : base(itemId)
    {
    }

    public List<ConcessionGroupProxy> ConcessionGroups { get; private set; }

    protected override void CommonCTOR()
    {
      ConcessionGroups = this.ChildrenOfTemplateType<ConcessionGroupProxy>(ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionGroup.Template);
      
    }
  }
}