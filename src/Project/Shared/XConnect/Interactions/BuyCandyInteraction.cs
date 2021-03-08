using Shared.Models.SitecoreCinema.Collection;
using Sitecore.XConnect;
using System;

namespace Shared.XConnect.Interactions
{
  public class BuyCandyInteraction : _interactionBase
  {
    public BuyCandyInteraction(Contact contact) : base(contact)
    {
    }

    public BuyCandyInteraction(string identifier) : base(identifier)
    {
    }

    public override async void InteractionBody()
    {
      if (XConnectContact != null)
      {
        var interaction = new Interaction(XConnectContact, InteractionInitiator.Contact, Const.XConnect.Channels.BoughtCandy, "");
        Client.SetFacet(interaction, CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = Const.XConnect.CinemaId.Theater22 });

        var eventItem = new BuyConcessionOutcome(DateTime.UtcNow, Const.SitecoreCinema.CurrencyCode, Const.SitecoreCinema.ConcessionPrices.PopCorn);
        interaction.Events.Add(eventItem);
        
        Client.AddInteraction(interaction);

        await Client.SubmitAsync();
      }
      else
      {
        Sitecore.Diagnostics.Log.Error("Contact was null", this);
      }
    }
  }
}