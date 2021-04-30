using LearnEXM.Feature.SitecoreCinema.Models.Proxies.ConcessionStand;
using LearnEXM.Foundation.CollectionModel.Builder;
using LearnEXM.Foundation.CollectionModel.Builder.Interactions;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Outcomes;
using Sitecore.XConnect;
using System;

namespace LearnEXM.Feature.SitecoreCinema.Interactions
{
  public class ConcessionStandInteraction : _interactionBase
  {
    public ConcessionStandInteraction(Sitecore.Analytics.Tracking.Contact trackingContact, Guid priceitem, Guid productitem) : base(trackingContact)
    {
      PriceItemGuid = priceitem;
      ProductItemGuid = productitem;
    }

    public Guid PriceItemGuid { get; }
    public Guid ProductItemGuid { get; }

    public override void InteractionBody()
    {
      if (XConnectContact != null)
      {
        var productItem = new ConcessionItemProxy(ProductItemGuid);
        var priceItem = new ConcessionPriceItemProxy(PriceItemGuid);
        if (productItem != null && priceItem != null)
        {
          decimal? priceValue = priceItem.CostField.DecimalValue;

          if (priceValue != null)
          {
            var interaction = new Interaction(XConnectContact, InteractionInitiator.Contact, CollectionConst.XConnect.Channels.ConcessionStand, string.Empty);

            Client.SetFacet(interaction, CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = CollectionConst.XConnect.CinemaId.Theater22 });

            var eventItem = new BuyConcessionOutcome(DateTime.UtcNow, CollectionConst.SitecoreCinema.CurrencyCode,(decimal)priceValue);
            interaction.Events.Add(eventItem);

            Client.AddInteraction(interaction);
          }
          else
          {
            Sitecore.Diagnostics.Log.Error(ProjectConst.Logging.prefix + "Price value was null", this);
          }
        }
        else
        {
          Sitecore.Diagnostics.Log.Error(ProjectConst.Logging.prefix + "ProductItem or PriceItem was null", this);
        }
      }
      else
      {
        Sitecore.Diagnostics.Log.Error("Contact was null", this);
      }
    }
  }
}