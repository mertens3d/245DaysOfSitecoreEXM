using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Outcomes;
using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.Interactions
{
  public class BuyCandyInteraction : _interactionBase
  {
    public BuyCandyInteraction(Contact contact) : base(contact)
    {
    }

    public BuyCandyInteraction(string identifier) : base(identifier)
    {
    }

    public BuyCandyInteraction(Sitecore.Analytics.Tracking.Contact trackingContact) : base(trackingContact)
    {
    }

    public override void InteractionBody()
    {
      if (XConnectContact != null)
      {
        var interaction = new Interaction(XConnectContact, InteractionInitiator.Contact, CollectionConst.XConnect.Channels.BoughtCandy, string.Empty);

        Client.SetFacet(interaction, CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = CollectionConst.XConnect.CinemaId.Theater22 });

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