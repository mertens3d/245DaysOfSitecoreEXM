using Shared.Models;
using Shared.Models.SitecoreCinema;
using Sitecore.Data;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.XConnect.Helpers
{
  public class KnownDataHelper
  {
    public async Task<KnownDataXConnect> GetKnownDataViaTrackerAsync(Sitecore.Analytics.Tracking.Contact trackingContact)
    {
      KnownDataXConnect toReturn = null;

      XConnectConfigHelper configHelper = new XConnectConfigHelper();
      XConnectClientConfiguration cfg = await configHelper.ConfigureClient();
      using (var Client = new XConnectClient(cfg))
      {
        try
        {
          var xConnectClientHelper = new XConnectClientHelper(Client);
          var identifierForSitecoreCinema = trackingContact.Identifiers.FirstOrDefault(x => x.Source == Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema);

          if (identifierForSitecoreCinema != null)
          {
            var xConnectContact = await xConnectClientHelper.GetXConnectContactByIdentifierAsync(Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, identifierForSitecoreCinema.Identifier);
            if (xConnectContact != null)
            {
              toReturn = GetKnownDataFromXConnectContact(xConnectContact);
            }
          }
        }
        catch (XdbExecutionException ex)
        {
          Sitecore.Diagnostics.Log.Error(Const.Logger.CinemaPrefix + ex.Message, this);
        }
      }

      return toReturn;
    }

    public async Task<KnownDataXConnect> GetKnownDataByIdentifierViaXConnect(string Identifier)
    {
      KnownDataXConnect toReturn = null;

      XConnectConfigHelper configHelper = new XConnectConfigHelper();
      XConnectClientConfiguration cfg = await configHelper.ConfigureClient();
      using (var Client = new XConnectClient(cfg))
      {
        try
        {
          var xConnectClientHelper = new XConnectClientHelper(Client);
          var xConnectContact = await xConnectClientHelper.GetXConnectContactByIdentifierAsync(Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, Identifier);
          if (xConnectContact != null)
          {
            toReturn = GetKnownDataFromXConnectContact(xConnectContact);
          }
        }
        catch (XdbExecutionException ex)
        {
          Sitecore.Diagnostics.Log.Error(Const.Logger.CinemaPrefix + ex.Message, this);
        }
      }

      return toReturn;
    }

    private string GetChannelName(Guid channelId)
    {
      var toReturn = string.Empty;

      try
      {
        var sitecoreItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(channelId));
        if (sitecoreItem != null)
        {
          toReturn = sitecoreItem.DisplayName;
        }
        else
        {
          toReturn = $"channel {channelId.ToString()} not found in Sitecore";
        }
      }
      catch (Exception ex)
      {
        Sitecore.Diagnostics.Log.Error(Const.Logger.CinemaPrefix + ex.Message, this);
      }

      return toReturn;
    }

    private List<InteractionProxy> GetKnownInteractions(Contact xconnectContact)
    {
      var toReturn = new List<InteractionProxy>();

      if (xconnectContact?.Interactions != null && xconnectContact.Interactions.Any())
      {
        foreach (var interaction in xconnectContact.Interactions)
        {
          toReturn.Add(new InteractionProxy()
          {
            ChannelId = interaction.ChannelId,

            RawInteraction = interaction,
            Events = interaction.Events,
            DeviceProfile = interaction.DeviceProfile,
            StartDateTime = interaction.StartDateTime,
            EndDateTime = interaction.EndDateTime,
            InitiatorStr = interaction.Initiator.ToString(),
            Id = interaction.Id,
            Duration = interaction.Duration,
            CampaignId = interaction.CampaignId,
          });
        }
      }

      return toReturn;
    }

    public KnownDataXConnect GetKnownDataFromXConnectContact(Contact xconnectContact)
    {
      var knownData = new KnownDataXConnect();
      if (xconnectContact != null)
      {
        knownData.ContactId = xconnectContact.Id;

        knownData.IsKnown = xconnectContact.IsKnown;

        knownData.PersonalInformationDetails = xconnectContact.GetFacet<PersonalInformation>();
        knownData.VisitorInfoMovie = xconnectContact.GetFacet<CinemaVisitorInfo>();

        knownData.KnownInteractions = GetKnownInteractions(xconnectContact);

        knownData.Identifiers = xconnectContact.Identifiers.ToList();
          
          
        //  .ToList().Select(x => new IdentifierSourcePair()
        //{
        //  Identifier = x.Identifier,
        //  Source = x.Source
        //}).ToList();

        knownData.IsKnown = xconnectContact.IsKnown;
      }
      else
      {
        Sitecore.Diagnostics.Log.Error(Const.Logger.CinemaPrefix + "Contact was null", this);
      }
      return knownData;
    }

    public void AppendCurrentContextData(KnownDataXConnect knownDataXConnect, Database database)
    {
      if (knownDataXConnect != null && database != null)
      {
        foreach (var interaction in knownDataXConnect.KnownInteractions)
        {
          if (interaction.ChannelId != Guid.Empty)
          {
            interaction.ChannelName = GetChannelName(interaction.ChannelId);
          }
          else
          {
            Sitecore.Diagnostics.Log.Error($"Interaction guid was empty", this);
          }
        }
      }
    }
  }
}