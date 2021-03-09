using LearnEXM.Feature.WhatWeKnowAboutYou.Models;
using LearnEXM.Foundation.CollectionModel.Builder;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.Data;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Feature.WhatWeKnowAboutYou.Helpers
{
  public class KnownDataHelper
  {
    private IXConnectFacets XConnectFacets { get; set; }

    public void AppendCurrentContextData(KnownData knownDataXConnect, Database database)
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

    public KnownData GetKnownDataFromTrackingContact()
    {
      var toReturn = new KnownData();

      return toReturn;
    }

    public KnownData GetKnownDataFromXConnectContact(Sitecore.Analytics.Tracking.Contact trackingContact)
    {
      var knownData = new KnownData();
      if (trackingContact != null)
      {
        XConnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");

        knownData.VisitorInfoMovie = XConnectFacets.Facets[CinemaVisitorInfo.DefaultFacetKey] as CinemaVisitorInfo;
        knownData.PersonalInformationDetails = XConnectFacets.Facets[PersonalInformation.DefaultFacetKey] as PersonalInformation;
        knownData.EmailAddressList = XConnectFacets.Facets[EmailAddressList.DefaultFacetKey] as EmailAddressList;

        //knownData.ContactId = trackingContact.Id;
        //knownData.IsKnown = trackingContact.IsKnown;
        //knownData.KnownInteractions = GetKnownInteractions(trackingContact);

        //knownData.Identifiers = trackingContact.Identifiers.ToList();

        //knownData.IsKnown = trackingContact.IsKnown;
      }
      else
      {
        Sitecore.Diagnostics.Log.Error(CollectionConst.Logger.CinemaPrefix + "Contact was null", this);
      }
      return knownData;
    }

    public KnownData GetKnownDataViaTracker(Sitecore.Analytics.Tracking.Contact trackingContact)
    {
      KnownData toReturn = null;

      //XConnectConfigHelper configHelper = new XConnectConfigHelper();
      using (XConnectClient xConnectClient = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
      {
        try
        {
          //var xConnectClientHelper = new XConnectClientHelper(Client);
          //var identifierForSitecoreCinema = trackingContact.Identifiers.FirstOrDefault(x => x.Source == Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema);

          toReturn = new KnownData();
          if (trackingContact != null && trackingContact.IdentificationLevel == Sitecore.Analytics.Model.ContactIdentificationLevel.Known)
          {
            var AnyIdentifier = Tracker.Current.Contact.Identifiers.FirstOrDefault();
            var identifiedReference = new IdentifiedContactReference(AnyIdentifier.Source, AnyIdentifier.Identifier);
            var expandOptions = new ExpandOptions(new[]{
              PersonalInformation.DefaultFacetKey,
              //EmailAddressList.DefaultFacetKey,
              //CinemaInfo.DefaultFacetKey
            });

            Contact XConnectContact = xConnectClient.Get(identifiedReference, expandOptions);

            XConnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");

            if (XConnectFacets != null)
            {
              toReturn.PersonalInformationDetails = XConnectFacets.Facets[PersonalInformation.DefaultFacetKey] as PersonalInformation;
              //toReturn.EmailAddressList = XConnectFacets.Facets[EmailAddressList.DefaultFacetKey] as EmailAddressList;
              //toReturn.VisitorInfoMovie = XConnectFacets.Facets[CinemaVisitorInfo.DefaultFacetKey] as CinemaVisitorInfo;
            }

            //knownData.ContactId = trackingContact.Id;
            //knownData.IsKnown = trackingContact.IsKnown;
            //knownData.KnownInteractions = GetKnownInteractions(trackingContact);

            //knownData.Identifiers = trackingContact.Identifiers.ToList();

            //knownData.IsKnown = trackingContact.IsKnown;
          }
          else
          {
            Sitecore.Diagnostics.Log.Error(CollectionConst.Logger.CinemaPrefix + "Contact was null", this);
          }
        }
        catch (XdbExecutionException ex)
        {
          Sitecore.Diagnostics.Log.Error(CollectionConst.Logger.CinemaPrefix + ex.Message, this);
        }
      }

      return toReturn;
    }

    //public async Task<KnownDataXConnect> GetKnownDataByIdentifierViaXConnect(string Identifier)
    //{
    //  KnownDataXConnect toReturn = null;

    //  XConnectConfigHelper configHelper = new XConnectConfigHelper();
    //  XConnectClientConfiguration cfg = await configHelper.ConfigureClient();
    //  using (var Client = new XConnectClient(cfg))
    //  {
    //    try
    //    {
    //      var xConnectClientHelper = new XConnectClientHelper(Client);
    //      var xConnectContact = await xConnectClientHelper.GetXConnectContactByIdentifierAsync(Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, Identifier);
    //      if (xConnectContact != null)
    //      {
    //        toReturn = GetKnownDataFromXConnectContact(xConnectContact);
    //      }
    //    }
    //    catch (XdbExecutionException ex)
    //    {
    //      Sitecore.Diagnostics.Log.Error(Const.Logger.CinemaPrefix + ex.Message, this);
    //    }
    //  }

    //  return toReturn;
    //}

    private string GetChannelName(Guid channelId)
    {
      var toReturn = string.Empty;

      try
      {
        var sitecoreItem = Sitecore.Context.Database.GetItem(new ID(channelId));
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
        Sitecore.Diagnostics.Log.Error(CollectionConst.Logger.CinemaPrefix + ex.Message, this);
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
  }
}