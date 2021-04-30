using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies.ConcessionStand
{
  public class ConcessionPriceItemProxy : _baseItemProxy
  {
    public SingleLineFieldProxy CostField { get; private set; }
    public SingleLineFieldProxy DescriptionField { get; private set; }
    protected override ID AssociatedTemplateID { get; } = ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionPrice.Template;
    public ConcessionItemProxy ParentProductItemProxy { get; set; }

    public ConcessionPurchaseOrder ConcessionPurchaseOrder
    {
      get
      {
        return new ConcessionPurchaseOrder
        {
          PriceItemID = Item.ID,
          ParentProductId = ParentProductItemProxy.ItemId
        };
      }
    }

    public string PurchaseLink
    {
      get
      {
        return ProjectConst.Links.SitecoreCinema.Lobby.BuyConcessions + "?priceitem=" + ConcessionPurchaseOrder.PriceItemID.Guid.ToString() + "&productitem=" + ConcessionPurchaseOrder.ParentProductId.Guid.ToString();
      }
    }

    protected override void CommonCTOR()
    {
      CostField = new SingleLineFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionPrice.Price);
      DescriptionField = new SingleLineFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.ConcessionPrice.DescriptionField);
    }

    internal void SetParentProductProxy(ConcessionItemProxy parentConcessionItem)
    {
      ParentProductItemProxy = parentConcessionItem;
    }
  }
}