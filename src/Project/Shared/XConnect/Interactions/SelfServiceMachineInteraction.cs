using Shared.Models.SitecoreCinema.Collection;
using Sitecore.XConnect;
using System;

namespace Shared.XConnect.Interactions
{
  public class SelfServiceMachineInteraction : _interactionBase
  {
    public SelfServiceMachineInteraction(string identifier) : base(identifier)
    {
    }

    public override async void InteractionBody()
    {
      //   ___       _                      _   _               _  _   _
      //  |_ _|_ __ | |_ ___ _ __ __ _  ___| |_(_) ___  _ __  _| || |_/ |
      //   | || '_ \| __/ _ \ '__/ _  |/ __| __| |/ _ \| '_ \|_  ..  _| |
      //   | || | | | ||  __/ | | (_| | (__| |_| | (_) | | | |_      _| |
      //  |___|_| |_|\__\___|_|  \__,_|\___|\__|_|\___/|_| |_| |_||_| |_|

      // You cycle to the nearest Sitecore Cinema (which has great bicycle storage facilities, by the way)
      // and use a self service machine to buy a ticket. You swipes your loyalty card - the machine
      // immediately sends this interaction to xConnect. Because you're a loyalty card member
      // you don't even pay at this point!

      if (Contact != null)
      {
        var interaction = new Interaction(Contact, InteractionInitiator.Contact, Const.XConnect.Channels.BoughtTicket, ""); // Guid should be from a channel in sitecore

        Client.SetFacet(interaction, CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = Const.XConnect.CinemaId.Theater22 });

        interaction.Events.Add(new UseSelfService(DateTime.UtcNow));

        Client.AddInteraction(interaction);

        await Client.SubmitAsync();
      }
    }
  }
}