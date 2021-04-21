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

    public string[] AllFacetKeys { get; set; } = new[] {
              CinemaInfo.DefaultFacetKey,
              CinemaVisitorInfo.DefaultFacetKey,
              EmailAddressList.DefaultFacetKey,
              PersonalInformation.DefaultFacetKey,
              CinemaDetails.DefaultFacetKey,
    };

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
            var expandOptions = new ContactExpandOptions(AllFacetKeys)
            {
              Interactions = new RelatedInteractionsExpandOptions()
              {
                StartDateTime = DateTime.MinValue,
                Limit = int.MaxValue
              }

              //Interactions = new RelatedInteractionsExpandOptions(AllFacetKeys)
            };

            Contact XConnectContact = xConnectClient.Get(identifiedReference, expandOptions);

            XConnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");

            if (XConnectFacets != null)
            {
              var facetHelper = new FacetHelper(XConnectFacets);

              toReturn.FacetData.CinemaDetails = facetHelper.SafeGetCreateFacet<CinemaDetails>(CinemaDetails.DefaultFacetKey);
              toReturn.FacetData.CinemaInfo = facetHelper.SafeGetCreateFacet<CinemaInfo>(CinemaInfo.DefaultFacetKey);

              if (toReturn.FacetData.CinemaInfo == null)
              {
                toReturn.FacetData.CinemaInfo = new CinemaInfo();
              }

              toReturn.FacetData.CinemaVisitorInfo = facetHelper.SafeGetCreateFacet<CinemaVisitorInfo>(CinemaVisitorInfo.DefaultFacetKey);
              toReturn.FacetData.EmailAddressList = facetHelper.SafeGetCreateFacet<EmailAddressList>(EmailAddressList.DefaultFacetKey);

              toReturn.FacetData.PersonalInformationDetails = facetHelper.SafeGetCreateFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey);
            }

            //knownData.ContactId = trackingContact.Id;
            //knownData.IsKnown = trackingContact.IsKnown;
            toReturn.KnownInteractions = GetKnownInteractions(XConnectContact, xConnectClient);

            toReturn.Identifiers = Tracker.Current.Contact.Identifiers.ToList();

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
            CustomValues = item.CustomValues
          });
        }
      }

      return toReturn;
    }

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
  }
}