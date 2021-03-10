using LearnEXM.Foundation.CollectionModel.Builder.Models.Events;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
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
      if (TrackingContact != null && XConnectContact != null)
      {
        var interaction = new Interaction(XConnectContact, InteractionInitiator.Contact, CollectionConst.XConnect.Channels.BoughtTicket, string.Empty);

        //var contact = Client.Get<Contact>(IdentifiedContactReference, new Sitecore.XConnect.ExpandOptions(PersonalInformation.DefaultFacetKey));

        Client.SetFacet(interaction, CinemaInfoFacet.DefaultFacetKey, new CinemaInfoFacet() { CinimaId = CollectionConst.XConnect.CinemaId.Theater22 });

        interaction.Events.Add(new UseSelfServiceEvent(DateTime.UtcNow));

        Client.AddInteraction(interaction);
      }
      else
      {
        Sitecore.Diagnostics.Log.Error("Tracking contact or Xconnect contact is null", this);
      }
    }
  }
}