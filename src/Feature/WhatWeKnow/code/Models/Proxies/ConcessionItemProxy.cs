using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data;
using System.Collections.Generic;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies
{
  public class ConcessionItemProxy : _baseItemProxy
  {
    public ConcessionItemProxy()
    {
      // for generics instantiation only
    }

    public SingleLineFieldProxy ProductName { get; set; }

    public List<ConcessionPriceItemProxy> ConcessionPrices{get;set;}
    public ImageFieldProxy ProductLogo { get; set; }
    protected override ID AssociatedTemplateId { get;  } = ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionProduct.Template;
    public ConcessionItemProxy(ID itemId) : base(itemId)
    {
    }

    protected override void CommonCTOR()
    {
      ProductName = new SingleLineFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.MovieData.MovieName);
      ProductLogo = new ImageFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.MovieData.Poster);
      ConcessionPrices = ChildrenOfTemplateType<ConcessionPriceItemProxy>();
    }
  }
}