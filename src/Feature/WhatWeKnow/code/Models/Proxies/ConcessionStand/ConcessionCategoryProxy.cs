using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies.ConcessionStand
{
  public class ConcessionCategoryProxy : _baseItemProxy
  {
    public ConcessionCategoryProxy()
    {
      // for generics only
    }

    public ConcessionCategoryProxy(Item item) : base(item)
    {
    }

    public List<ConcessionItemProxy> ChildConcessionItems { get; private set; }
    public List<ConcessionSubCategoryProxy> ChildSubCategoryItems { get; private set; }
    public SingleLineFieldProxy GroupNameFieldProxy { get; private set; }
    public CheckBoxFieldProxy IsAdultFieldProxy { get; private set; }
    protected override ID AssociatedTemplateID { get; } = ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionCategory.Template;

    protected override void CommonCTOR()
    {
      ChildConcessionItems = ChildrenOfTemplateType<ConcessionItemProxy>();
      ChildSubCategoryItems = ChildrenOfTemplateType<ConcessionSubCategoryProxy>();
      IsAdultFieldProxy = new CheckBoxFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionCategory.IsAdultField);
      GroupNameFieldProxy = new SingleLineFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionCategory.GroupName);
    }
  }
}