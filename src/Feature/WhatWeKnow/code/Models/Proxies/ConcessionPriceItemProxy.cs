using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies
{
  public class ConcessionPriceItemProxy : _baseItemProxy
  {
    public SingleLineFieldProxy CostField { get; private set; }
    public ImageFieldProxy DescriptionField { get; private set; }
    protected override ID AssociatedTemplateId { get; } = ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionProduct.Template;


    protected override void CommonCTOR()
    {
      CostField = new SingleLineFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionPrice.Price);
      DescriptionField = new ImageFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionPrice.DescriptionField);
    }
  }
}