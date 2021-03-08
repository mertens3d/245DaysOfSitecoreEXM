using Shared.Models;
using Shared.Models.SitecoreCinema.Collection;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;

namespace Shared.XConnect.Interactions
{
  public class SelfServiceMachineInteraction : _interactionBase
  {

    public SelfServiceMachineInteraction(string identifier) : base(identifier) { }
    public SelfServiceMachineInteraction(Sitecore.Analytics.Model.Entities.ContactIdentifier identifierSourcePair) : base(identifierSourcePair.Identifier)
    {
    }

    public SelfServiceMachineInteraction(Sitecore.Analytics.Tracking.Contact trackingContact):base(trackingContact)
    {
    }


    public override  void InteractionBody()
    {
      if (TrackingContact != null && XConnectContact != null)
      {
        var interaction = new Interaction(XConnectContact, InteractionInitiator.Contact, Const.XConnect.Channels.BoughtTicket, string.Empty); 

        //var contact = Client.Get<Contact>(IdentifiedContactReference, new Sitecore.XConnect.ExpandOptions(PersonalInformation.DefaultFacetKey));

        Client.SetFacet(interaction, CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = Const.XConnect.CinemaId.Theater22 });

        interaction.Events.Add(new UseSelfService(DateTime.UtcNow));

        Client.AddInteraction(interaction);

      }
      else
      {
        Sitecore.Diagnostics.Log.Error("Tracking contact or Xconnect contact is null", this);
      }
    }
  }
}