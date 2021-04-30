using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data;
using Sitecore.Data.Items;
using System.Collections.Generic;
using static LearnEXM.Foundation.CollectionModel.Builder.CollectionConst.SitecoreCinema;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies
{
  public class ConcessionSubCategoryProxy : _baseItemProxy
  {
    public ConcessionSubCategoryProxy()
    {
      // for generics only
    }
    public ConcessionSubCategoryProxy(Item item) : base(item)
    {
    }

    public List<ConcessionItemProxy> ChildConcessionItems { get; private set; }
    public SingleLineFieldProxy NameFieldProxy { get; private set; }
    public List<ConcessionPriceItemProxy> ConcessionPrices { get; private set; }
    protected override ID AssociatedTemplateId { get; } = ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionSubCategory.Template;

    protected override void CommonCTOR()
    {
      ChildConcessionItems = this.ChildrenOfTemplateType<ConcessionItemProxy>();
      NameFieldProxy = new SingleLineFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionSubCategory.NameField);
      ConcessionPrices = ChildrenOfTemplateType<ConcessionPriceItemProxy>();
    }
  }
}