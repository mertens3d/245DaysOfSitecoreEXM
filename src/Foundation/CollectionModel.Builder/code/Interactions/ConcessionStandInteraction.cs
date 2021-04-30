using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Outcomes;
using LearnEXM.Foundation.xConnectHelper.Helpers;
using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.Interactions
{
  public class ConcessionStandInteraction : _interactionBase
  {
    public ConcessionStandInteraction(Sitecore.Analytics.Tracking.Contact trackingContact, Guid priceitem, Guid productitem) : base(trackingContact)
    {
      PriceitemGuid = priceitem;
      ProductItemGuid = productitem;
    }

    public Guid PriceitemGuid { get; }
    public Guid ProductItemGuid { get; }

    public override void InteractionBody()
    {
      if (XConnectContact != null)
      {
        var interaction = new Interaction(IdentifiedContactReference, InteractionInitiator.Contact, CollectionConst.XConnect.Channels.ConcessionStand, string.Empty);

        var facetHelper = new FacetEditHelper(XConnectFacets);

        var cinemaInfoFacet = facetHelper.SafeGetFacet<CinemaInfo>(CollectionConst.FacetKeys.CinemaInfo);

        if (cinemaInfoFacet != null)
        {
          Client.SetFacet(IdentifiedContactReference, CinemaInfo.DefaultFacetKey, cinemaInfoFacet);
        }

        var visitorInfoFacet = facetHelper.SafeGetFacet<CinemaVisitorInfo>(CollectionConst.FacetKeys.CinemaVisitorInfo);
        if (visitorInfoFacet != null)
        {
          Client.SetFacet(IdentifiedContactReference, CollectionConst.FacetKeys.CinemaVisitorInfo, visitorInfoFacet);
        }

        var eventItem = new BuyConcessionOutcome(DateTime.UtcNow, CollectionConst.SitecoreCinema.CurrencyCode, CollectionConst.SitecoreCinema.ConcessionPrices.PopCorn);
        interaction.Events.Add(eventItem);

        Client.AddInteraction(interaction);
      }
      else
      {
        Sitecore.Diagnostics.Log.Error("Contact was null", this);
      }
    }
  }
}