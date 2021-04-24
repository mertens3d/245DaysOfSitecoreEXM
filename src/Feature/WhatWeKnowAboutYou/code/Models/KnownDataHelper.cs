using LearnEXM.Feature.WhatWeKnowAboutYou.Helpers;
using LearnEXM.Foundation.CollectionModel.Builder;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using Newtonsoft.Json;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.Data;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Serialization;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Feature.WhatWeKnowAboutYou.Models
{
  public class KnownDataHelper
  {
    public string[] AllFacetKeys { get; set; } = new[] {
              CinemaInfo.DefaultFacetKey,
              CinemaVisitorInfo.DefaultFacetKey,
              EmailAddressList.DefaultFacetKey,
              PersonalInformation.DefaultFacetKey,
              CinemaDetails.DefaultFacetKey,
    };

    private IXConnectFacets XConnectFacets { get; set; }

    public void AppendCurrentContextData(KnownData knownDataXConnect, Database database)
    {
      if (knownDataXConnect != null && database != null)
      {
        foreach (var interaction in knownDataXConnect.KnownInteractions)
        {
          if (interaction.ChannelId != Guid.Empty)
          {
            interaction.ChannelName = GetDisplayName(interaction.ChannelId);
            interaction.Events = interaction.Events;
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
    public KnownData GetKnownDataViaTracker(Sitecore.Analytics.Tracking.Contact trackingContact)
    {
      KnownData toReturn = null;

      using (XConnectClient xConnectClient = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
      {
        try
        {
          toReturn = new KnownData();

          Contact XConnectContact = IdentifyContact(trackingContact, xConnectClient);

          XConnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");

          toReturn.FacetData = GatherFacetData(XConnectFacets);

          toReturn.KnownInteractions = GetKnownInteractions(XConnectContact, xConnectClient);

          toReturn.Identifiers = Tracker.Current.Contact.Identifiers.ToList();
        }
        catch (XdbExecutionException ex)
        {
          Sitecore.Diagnostics.Log.Error(CollectionConst.Logger.CinemaPrefix + ex.Message, this);
        }
      }

      return toReturn;
    }

    private KnownDataFacets GatherFacetData(IXConnectFacets xConnectFacets)
    {
      var toReturn = new KnownDataFacets();

      if (XConnectFacets != null)
      {
        var facetHelper = new FacetHelper(XConnectFacets);

        toReturn.CinemaDetails = facetHelper.SafeGetCreateFacet<CinemaDetails>(CinemaDetails.DefaultFacetKey);
        toReturn.CinemaInfo = facetHelper.SafeGetCreateFacet<CinemaInfo>(CinemaInfo.DefaultFacetKey);

        if (toReturn.CinemaInfo == null)
        {
          toReturn.CinemaInfo = new CinemaInfo();
        }

        toReturn.CinemaVisitorInfo = facetHelper.SafeGetCreateFacet<CinemaVisitorInfo>(CinemaVisitorInfo.DefaultFacetKey);
        toReturn.EmailAddressList = facetHelper.SafeGetCreateFacet<EmailAddressList>(EmailAddressList.DefaultFacetKey);

        toReturn.PersonalInformationDetails = facetHelper.SafeGetCreateFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey);
      }

      return toReturn;
    }

    private string GetDisplayName(Guid itemId)
    {
      var toReturn = string.Empty;

      try
      {
        var sitecoreItem = Sitecore.Context.Database.GetItem(new ID(itemId));
        if (sitecoreItem != null)
        {
          toReturn = sitecoreItem.DisplayName;
        }
        else
        {
          toReturn = "{not found}";
        }
      }
      catch (Exception ex)
      {
        Sitecore.Diagnostics.Log.Error(CollectionConst.Logger.CinemaPrefix + ex.Message, this);
      }

      return toReturn;
    }

    private List<EventRecordProxy> GetEvents(EventCollection events)
    {
      List<EventRecordProxy> toReturn = new List<EventRecordProxy>();

      if (events != null)
      {
        foreach (var item in events)
        {
          toReturn.Add(new EventRecordProxy()
          {
            TypeName = item.GetType().Name,
            CustomValues = item.CustomValues,
            TimeStamp = item.Timestamp,
            ItemId = item.ItemId,
            ItemDisplayName = GetDisplayName(item.ItemId),
            Duration = item.Duration
          }); ;
        }
      }

      toReturn.Sort((x, y) => x.TimeStamp.CompareTo(y.TimeStamp));
      toReturn.Reverse();

      return toReturn;
    }

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
    
    //public async Task<KnownDataXConnect> GetKnownDataByIdentifierViaXConnect(string Identifier)
    //{
    //  KnownDataXConnect toReturn = null;
    private List<InteractionProxy> GetKnownInteractions(Contact xconnectContact, XConnectClient xConnectClient)
    {
      var toReturn = new List<InteractionProxy>();

      //xConnectClient.Get<Interaction>(interactionRef,
      //    new Sitecore.XConnect.InteractionExpandOptions(AllFacetKeys));

      if (xconnectContact?.Interactions != null && xconnectContact.Interactions.Any())
      {
        var ContractResolver = new XdbJsonContractResolver(xConnectClient.Model, true, true);

        var serializerSettings = new JsonSerializerSettings
        {
          ContractResolver = ContractResolver,
          DateTimeZoneHandling = DateTimeZoneHandling.Utc,
          DefaultValueHandling = DefaultValueHandling.Ignore,
          Formatting = Formatting.Indented
        };

        foreach (var interaction in xconnectContact.Interactions)
        {
          toReturn.Add(new InteractionProxy()
          {
            ChannelId = interaction.ChannelId,
            ChannelName = GetDisplayName(interaction.ChannelId),

            RawInteraction = interaction,
            Events = interaction.Events,
            EventsB = GetEvents(interaction.Events),
            DeviceProfile = interaction.DeviceProfile,
            StartDateTime = interaction.StartDateTime,
            EndDateTime = interaction.EndDateTime,
            InitiatorStr = interaction.Initiator.ToString(),
            Id = interaction.Id,
            Duration = interaction.Duration,
            CampaignId = interaction.CampaignId,
            SerializedAsJson = JsonConvert.SerializeObject(interaction, serializerSettings)
          });
        }
      }

      return toReturn;
    }

    private Contact IdentifyContact(Sitecore.Analytics.Tracking.Contact trackingContact, XConnectClient xConnectClient)
    {
      Contact toReturn = null;

      if (trackingContact != null && trackingContact.IdentificationLevel == Sitecore.Analytics.Model.ContactIdentificationLevel.Known)
      {
        var AnyIdentifier = Tracker.Current.Contact.Identifiers.FirstOrDefault();
        var identifiedReference = new IdentifiedContactReference(AnyIdentifier.Source, AnyIdentifier.Identifier);
        var expandOptions = new ContactExpandOptions(AllFacetKeys)
        {
          Interactions = new RelatedInteractionsExpandOptions()
          {
            StartDateTime = DateTime.MinValue,
            Limit = int.MaxValue
          }
        };
      
        toReturn = xConnectClient.Get(identifiedReference, expandOptions);
      }
      else
      {
        Sitecore.Diagnostics.Log.Error(CollectionConst.Logger.CinemaPrefix + "Contact was null", this);
      }


      return toReturn;
    }
  }
}