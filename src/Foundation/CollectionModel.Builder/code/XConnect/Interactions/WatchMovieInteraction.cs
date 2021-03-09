﻿using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Outcomes;
using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.XConnect.Interactions
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

    public override void InteractionBody()
    {
      if (XConnectContact != null)
      {
        var interaction = new Interaction(XConnectContact, InteractionInitiator.Contact, CollectionConst.XConnect.Channels.WatchedMovie, string.Empty);

        interaction.Events.Add(new WatchMovieOutcome(DateTime.UtcNow, CollectionConst.SitecoreCinema.CurrencyCode, CollectionConst.SitecoreCinema.ConcessionPrices.WatchMovie)
        {
          EIDR = CollectionConst.XConnect.MovieEIDR.DieHard
        });

        Client.SetFacet(interaction, CinemaInfoFacet.DefaultFacetKey, new CinemaInfoFacet() { CinimaId = CollectionConst.XConnect.CinemaId.Theater22 });

        Client.AddInteraction(interaction);
      }
      else
      {
        Errors.Add("Contact was null");
      }
    }
  }
}