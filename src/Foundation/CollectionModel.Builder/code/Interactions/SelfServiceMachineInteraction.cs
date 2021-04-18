using LearnEXM.Foundation.CollectionModel.Builder.Models.Events;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.Interactions
{
  public class SelfServiceMachineInteraction : _interactionBase
  {
    public SelfServiceMachineInteraction(string identifier) : base(identifier)
    {
    }

    public SelfServiceMachineInteraction(Sitecore.Analytics.Model.Entities.ContactIdentifier identifierSourcePair) : base(identifierSourcePair.Identifier)
    {
    }

    public SelfServiceMachineInteraction(Sitecore.Analytics.Tracking.Contact trackingContact) : base(trackingContact)
    {
    }

    public override void InteractionBody()
    {


      XConnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");




        var interaction = new Interaction(IdentifiedContactReference, InteractionInitiator.Contact, CollectionConst.XConnect.Channels.BoughtTicket, string.Empty);

        //var contact = Client.Get<Contact>(IdentifiedContactReference, new Sitecore.XConnect.ExpandOptions(PersonalInformation.DefaultFacetKey));


        var cinemaInfoFacet = new CinemaInfo() { CinimaId = CollectionConst.XConnect.CinemaId.Theater22 };

        Client.SetFacet(IdentifiedContactReference, CinemaInfo.DefaultFacetKey, cinemaInfoFacet);

      interaction.Events.Add(new UseSelfServiceEvent(DateTime.UtcNow));

      Client.AddInteraction(interaction);
    }
  }
}