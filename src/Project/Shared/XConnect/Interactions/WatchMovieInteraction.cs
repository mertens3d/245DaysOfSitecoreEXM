using Shared.Models.SitecoreCinema.Collection;
using Sitecore.XConnect;
using System;

namespace Shared.XConnect.Interactions
{
  public class WatchMovieInteraction : _interactionBase
  {
    public WatchMovieInteraction(string identifier) : base(identifier)
    {
    }

    public override async void InteractionBody()
    {
      if (XConnectContact != null)
      {
        var interaction = new Interaction(XConnectContact, InteractionInitiator.Contact, Const.XConnect.Channels.WatchedMovie, "");

        interaction.Events.Add(new WatchMovie(DateTime.UtcNow, "Dkk", 100m)
        {
          EIDR = Const.XConnect.MovieEIDR.DieHard
        }
          );

        Client.SetFacet(interaction, CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = Const.XConnect.CinemaId.Theater22 });

        Client.AddInteraction(interaction);
        await Client.SubmitAsync();
      }
      else
      {
        Errors.Add("Contact was null");
      }
    }
  }
}