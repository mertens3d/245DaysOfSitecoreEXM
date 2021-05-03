using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories;
using LearnEXM.Foundation.xConnectHelper;
using LearnEXM.Foundation.xConnectHelper.Proxies;
using Sitecore.Data;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers.NodeBuilders
{
  public class InteractionsNodeBuilder
  {
    private WeKnowTreeOptions TreeOptions { get; set; }

    public InteractionsNodeBuilder(WeKnowTreeOptions treeOptions)
    {
      this.TreeOptions = treeOptions;
    }

    public WeKnowTreeNode Something(Contact xconnectContact, XConnectClient xConnectClient)
    {
      WeKnowTreeNode toReturn = null;

      var interactions = xconnectContact.Interactions.OrderByDescending(x => x.LastModified);
      if (interactions != null && interactions.Any())
      {
        toReturn = new WeKnowTreeNode("Interactions", TreeOptions);
        var objectToTreeNode = new ObjectToTreeNode(TreeOptions, xConnectClient);

        foreach (var interaction in interactions)
        {
          var nodeName = "Interaction";

          if (interaction.Id != null)
          {
            nodeName = GetDisplayName((Guid)interaction.ChannelId);
          }
          var interactionNode = objectToTreeNode.MakeTreeNodeFromObject(interaction, "Channel: " + nodeName);
          if (interactionNode != null)
          {
            interactionNode.AddRawNode(objectToTreeNode.SerializeObject(interaction));
          }
          toReturn.AddNode(interactionNode);
        }
      }

      return toReturn;
    }

    private string GetDisplayName(ID itemId)
    {
      var toReturn = string.Empty;

      try
      {
        var sitecoreItem = Sitecore.Context.Database.GetItem(itemId);
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

    private string GetDisplayName(Guid itemGuid)
    {
      return GetDisplayName(new ID(itemGuid));
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