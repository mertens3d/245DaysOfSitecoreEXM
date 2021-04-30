using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies.ConcessionStand
{
  public class ConcessionItemProxy : _baseItemProxy
  {
    public ConcessionItemProxy()
    {
      // for generics instantiation only
    }

    public SingleLineFieldProxy ProductNameField { get; set; }

    public List<ConcessionPriceItemProxy> ConcessionPrices { get; set; } = new List<ConcessionPriceItemProxy>();
    public ImageFieldProxy ProductLogo { get; set; }
    protected override ID AssociatedTemplateID { get; } = ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionProduct.Template;
    public ConcessionItemProxy(ID itemId) : base(itemId)
    {
    }

    public string BuyConcessionLink { get; set; }

    protected override void CommonCTOR()
    {
      ProductNameField = new SingleLineFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionProduct.ProductName);
      ProductLogo = new ImageFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionProduct.ProductLogo);
      ConcessionPrices = ChildrenOfTemplateType<ConcessionPriceItemProxy>();
      ProcessPriceData();
    }

    private void ProcessPriceData()
    {
      if(ConcessionPrices != null && ConcessionPrices.Any())
      {
        ConcessionPrices.ForEach(x => x.SetParentProductProxy(this));
      }
    }

    internal void IncludePrices(List<ConcessionPriceItemProxy> parentConcessionPrices)
    {
      if (parentConcessionPrices != null && parentConcessionPrices.Any())
      {
        ConcessionPrices.AddRange(parentConcessionPrices);
      }
      ProcessPriceData();
    }
  }
}