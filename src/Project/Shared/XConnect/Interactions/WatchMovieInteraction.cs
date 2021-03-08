using Shared.Models.SitecoreCinema.Collection;
using Sitecore.XConnect;
using System;

namespace Shared.XConnect.Interactions
{
  public class WatchMovieInteraction : _interactionBase
  {
    private Sitecore.Analytics.Tracking.Contact contact;

    public WatchMovieInteraction(string identifier) : base(identifier)
    {
    }

    public WatchMovieInteraction(Sitecore.Analytics.Tracking.Contact trackerContact) : base(trackerContact)
    {
     
    }

    public override  void InteractionBody()
    {
      if (XConnectContact != null)
      {
        var interaction = new Interaction(XConnectContact, InteractionInitiator.Contact, Const.XConnect.Channels.WatchedMovie, string.Empty);

        interaction.Events.Add(new WatchMovie(DateTime.UtcNow, Shared.Const.SitecoreCinema.CurrencyCode, Shared.Const.SitecoreCinema.ConcessionPrices.WatchMovie)
        {
          EIDR = Const.XConnect.MovieEIDR.DieHard
        });

        Client.SetFacet(interaction, CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = Const.XConnect.CinemaId.Theater22 });

        Client.AddInteraction(interaction);
      }
      else
      {
        Errors.Add("Contact was null");
      }
    }
  }
}