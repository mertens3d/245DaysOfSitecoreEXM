using LearnEXM.Foundation.xConnectHelper.Proxies;
using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.xConnectHelper.Helpers
{
  public class InteractionHelper
  {
    public List<InteractionProxy> GetKnownInteractions(Contact xconnectContact, XConnectClient xConnectClient)
    {
      var toReturn = new List<InteractionProxy>();

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
        Sitecore.Diagnostics.Log.Error(ProjectConstants.Logger.Prefix + ex.Message, this);
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
  }
}