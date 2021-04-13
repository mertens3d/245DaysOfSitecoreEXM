using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;

namespace Sitecore.Documentation
{
  public class UpdateHelper
  {
    public void RecordInteractionFacet(XConnectClient client, Contact knownContact)
    {
      var channelId = Const.XConnect.ChannelIds.OtherEvent;
      var offlineGoal = Const.XConnect.Goals.WatchedDemo;

      // Create a new interaction for that contact
      Interaction interaction = new Interaction(knownContact, InteractionInitiator.Brand, channelId, "");

      // add events - all interactions must have at least one event
      var xConnectEvent = new Goal(offlineGoal, DateTime.UtcNow);
      interaction.Events.Add(xConnectEvent);

      IpInfo ipInfo = new IpInfo("127.0.0.1");
      ipInfo.BusinessName = "Home";

      client.SetFacet<IpInfo>(interaction, IpInfo.DefaultFacetKey, ipInfo);

      // Add the contact and interaction
      client.AddInteraction(interaction);
    }

    public void PopulatePersonalInformationFacet(XConnectClient client, Contact knownContact)
    {
      PersonalInformation personalInformationFacet = new PersonalInformation();

      personalInformationFacet.FirstName = "Myrtle";
      personalInformationFacet.LastName = "McSitecore";
      personalInformationFacet.JobTitle = Const.XConnect.JobTitles.ProgrammerWriter; 

      client.SetFacet<PersonalInformation>(knownContact, PersonalInformation.DefaultFacetKey, personalInformationFacet);
    }
  }
}