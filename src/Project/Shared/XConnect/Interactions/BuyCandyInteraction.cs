using Shared.Models.SitecoreCinema.Collection;
using Sitecore.XConnect;
using System;

namespace Shared.XConnect.Interactions
{
  public class BuyCandyInteraction : _interactionBase
  {
    public BuyCandyInteraction(string identifier) : base(identifier)
    {
    }

    public override async void InteractionBody()
    {
      if (Contact != null)
      {
        var interaction = new Interaction(Contact, InteractionInitiator.Contact, Const.XConnect.Channels.BoughtCandy, "");
        Client.SetFacet(interaction, CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = Const.XConnect.CinemaId.Theater22 });

        interaction.Events.Add(new BuyConcessions(DateTime.UtcNow, "Dkk", 150m));

        Client.AddInteraction(interaction);

        await Client.SubmitAsync();
      }
    }
  }
}