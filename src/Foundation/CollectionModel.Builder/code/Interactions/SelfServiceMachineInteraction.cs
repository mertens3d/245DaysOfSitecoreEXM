using LearnEXM.Foundation.CollectionModel.Builder.Models.Events;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.xConnectHelper.Helpers;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.Interactions
{
  public class SelfServiceMachineInteraction : _interactionBase
  {
       public SelfServiceMachineInteraction(Sitecore.Analytics.Tracking.Contact trackingContact, MovieTicket movieTicket) : base(trackingContact)
    {
      MovieTicket = movieTicket;
    }

    private MovieTicket MovieTicket { get; }

    public override void InteractionBody()
    {

      var interaction = new Interaction(IdentifiedContactReference, InteractionInitiator.Contact, CollectionConst.XConnect.Channels.BoughtTicket, string.Empty);

      var facetHelper = new FacetHelper(null);//todo (XConnectContact);

      var cinemaInfoFacet = facetHelper.SafeGetCreateFacet<CinemaInfo>(CollectionConst.FacetKeys.CinemaInfo);
      
      if (cinemaInfoFacet != null)
      {
        new CinemaInfo() { CinimaId = CollectionConst.XConnect.CinemaId.Theater22 };
        Client.SetFacet(IdentifiedContactReference, CinemaInfo.DefaultFacetKey, cinemaInfoFacet);
      }

      var visitorInfoFacet = facetHelper.SafeGetCreateFacet<CinemaVisitorInfo>(CollectionConst.FacetKeys.CinemaVisitorInfo);
      if (visitorInfoFacet != null)
      {
        visitorInfoFacet.MovieTickets.Add(MovieTicket);
        Client.SetFacet(IdentifiedContactReference, CollectionConst.FacetKeys.CinemaVisitorInfo, visitorInfoFacet);
      }

      interaction.Events.Add(new UseSelfServiceEvent(DateTime.UtcNow));

      Client.AddInteraction(interaction);
    }
  }
}