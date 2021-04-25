using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using LearnEXM.Foundation.WhatWeKnowBullets.Models;
using LearnEXM.Foundation.xConnectHelper.Helpers;
using Newtonsoft.Json;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.Data;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Helpers
{
  public class KnownDataHelper
  {
    public KnownDataHelper(List<string> targetedFacetKeys, List<IFacetBulletFactory> customFacetKeyBulletFactories)
    {
      //TargetedFacetTypes = targetedFacetKeys;
      CustomFacetKeyBulletFactories = customFacetKeyBulletFactories;
      TargetedFacetKeys = targetedFacetKeys;
      //foreach (var type in targetedFacetKeys)
      //{
      //  PropertyInfo prop = type.GetProperty("DefaultFacetKey", BindingFlags.Static);

      //  if (prop != null)
      //  {
      //    string propValue = prop.GetValue(null) as string;
      //    if (!string.IsNullOrEmpty(propValue))
      //    {
      //      TargetedFacetKeys.Add(propValue);
      //    }
      //    else
      //    {
      //      Sitecore.Diagnostics.Log.Error(WhatWeKnowBulletsConstants.Logger.Prefix + type.Name + " did not have valid string value", this);
      //    }
      //  }
      //  else
      //  {
      //    Sitecore.Diagnostics.Log.Error(WhatWeKnowBulletsConstants.Logger.Prefix + type.Name + " does not have a 'DefaultFacetKey'", this);
      //  }
      //}
    }

    //private List<Type> TargetedFacetTypes { get; }
    private List<IFacetBulletFactory> CustomFacetKeyBulletFactories { get; }

    private List<string> TargetedFacetKeys { get; set; } = new List<string>();

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

          var xConnectHelper = new XConnectHelper(TargetedFacetKeys);

          IdentifiedContactReference IdentifiedContactReference = xConnectHelper.GetIdentifierFromTrackingContact(trackingContact);

          Contact XConnectContact = xConnectHelper.IdentifyKnownContact(IdentifiedContactReference);

          XConnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");

          toReturn.FacetData = GatherFacetData(XConnectFacets);

          toReturn.KnownInteractions = GetKnownInteractions(XConnectContact, xConnectClient);

          toReturn.Identifiers = Tracker.Current.Contact.Identifiers.ToList();
        }
        catch (XdbExecutionException ex)
        {
          Sitecore.Diagnostics.Log.Error(WhatWeKnowBulletsConstants.Logger.Prefix + ex.Message, this);
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

        foreach (var targetFacetKey in TargetedFacetKeys)
        {
          toReturn.BulletReports.Add(GetBulletReport(targetFacetKey, facetHelper));
        }
      }

      return toReturn;
    }

    private IBullet GetBulletReport(string targetFacetKey, FacetHelper facetHelper)
    {
      IBullet toReturn = null;

      var matchingFactory = GetMatchingFactory(targetFacetKey);
      if (matchingFactory != null)
      {
        var facet = facetHelper.GetFacetByKey(targetFacetKey);
        if (facet != null)
        {
          toReturn = matchingFactory.GetBullet(facet);
        }
      }

      return toReturn;
    }

    private List<IFacetBulletFactory> AllBuiltInBulletFactories
    {
      get
      {
        return new List<IFacetBulletFactory>
        {
          new EmailAddressListBulletFactory(),
          new PersonalInformationBulletFactory()
        };
      }
    }

    private IFacetBulletFactory GetMatchingFactory(string facetKey)
    {
      IFacetBulletFactory toReturn = null;

      toReturn = AllBuiltInBulletFactories.FirstOrDefault(x => x.AssociatedDefaultFacetKey.Equals(facetKey));
      if (toReturn == null && CustomFacetKeyBulletFactories != null)
      {
        toReturn = CustomFacetKeyBulletFactories.FirstOrDefault(x => x.AssociatedDefaultFacetKey.Equals(facetKey));
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
        Sitecore.Diagnostics.Log.Error(WhatWeKnowBulletsConstants.Logger.Prefix + ex.Message, this);
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
  }
}