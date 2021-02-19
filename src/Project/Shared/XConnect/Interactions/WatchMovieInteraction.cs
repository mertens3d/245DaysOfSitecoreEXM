using Sitecore.XConnect;
using SitecoreCinema.Model.Collection;
using System;

namespace Shared.XConnect.Interactions
{
  public class WatchMovieInteraction : _xconnectBase
  {
    public WatchMovieInteraction(string identifier) : base(identifier)
    {
    }

    public override async void InteractionBody()
    {
      if (Contact != null)
      {
        var interaction = new Interaction(Contact, InteractionInitiator.Contact, Const.XConnect.Channels.WatchedMovie, "");

        interaction.Events.Add(new WatchMovie(DateTime.UtcNow, "Dkk", 100m)
        {
          EIDR = Const.XConnect.MovieEIDR.DieHard
        }
          );

        Client.SetFacet(interaction, SitecoreCinema.Model.Collection.CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = Const.XConnect.CinemaId.Theater22 });

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