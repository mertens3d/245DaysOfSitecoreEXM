using Sitecore.Data;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies.ConcessionStand
{
  public class ConcessionPurchaseOrder
  {
    public ID ParentProductId { get; internal set; }
    public ID PriceItemID { get; internal set; }
  }
}