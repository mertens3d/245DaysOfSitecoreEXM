using Sitecore.XConnect;
using SitecoreCinema.Model.Collection;
using System;

namespace Shared.XConnect.Interactions
{
  public class BuyCandyInteraction : _xconnectBase
  {
    public BuyCandyInteraction(string identifier) : base(identifier)
    {
    }

    public override async void InteractionBody()
    {
      await PopulateContactDataAsync();

      if (Contact != null)
      {
        System.Console.WriteLine("Why hello again " + KnownData.details.FirstName + "!");
        System.Console.WriteLine("Candy? You got it.");

        var interaction = new Interaction(Contact, InteractionInitiator.Contact, Const.XConnect.Channels.BoughtCandy, "");
        Client.SetFacet(interaction, SitecoreCinema.Model.Collection.CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = Const.XConnect.CinemaId.Theater22 });

        interaction.Events.Add(new BuyConcessions(DateTime.UtcNow, "Dkk", 150m));

        Client.AddInteraction(interaction);

        await Client.SubmitAsync();
      }
    }
  }
}