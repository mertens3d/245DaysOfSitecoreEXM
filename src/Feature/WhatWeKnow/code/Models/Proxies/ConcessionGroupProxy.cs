using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies
{
  public class ConcessionGroupProxy : _baseItemProxy
  {
    public ConcessionGroupProxy()
    {
      // for generics only
    }
    public ConcessionGroupProxy(Item item) : base(item)
    {
    }

    public List<ConcessionItemProxy> ConcessionItems { get; private set; }
    public CheckBoxFieldProxy IsAdultFieldProxy { get; private set; }
    public SingleLineFieldProxy GroupNameFieldProxy { get; private set; }

    protected override void CommonCTOR()
    {
      ConcessionItems = this.ChildrenOfTemplateType<ConcessionItemProxy>(ProjectConst.Items.Templates.Feature.SitecoreCinema.Concession.Template);
      IsAdultFieldProxy = new CheckBoxFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionGroup.IsAdultField);
      GroupNameFieldProxy = new SingleLineFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionGroup.GroupName);
    }
  }
}