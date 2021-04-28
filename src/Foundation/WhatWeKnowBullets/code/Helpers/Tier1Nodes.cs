using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using LearnEXM.Foundation.xConnectHelper.Helpers;
using Newtonsoft.Json;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers
{
  public class Tier1Nodes
  {
    public Tier1Nodes(Sitecore.Analytics.Tracking.Contact trackingContact, List<string> targetedFacetKeys, List<IFacetNodeFactory> customFacetKeyBulletFactories)
    {
      this.XConnectFacets = trackingContact.GetFacet<IXConnectFacets>("XConnectFacets"); ;
      TargetedFacetKeys = targetedFacetKeys;
      CustomFacetKeyBulletFactories = customFacetKeyBulletFactories;
    }

    public List<string> TargetedFacetKeys { get; }
    private List<IFacetNodeFactory> CustomFacetKeyBulletFactories { get; }
    private IXConnectFacets XConnectFacets { get; set; }
    public ITreeNode EventsNode(List<xConnectHelper.Proxies.EventRecordProxy> events)
    {
      var eventsNode = new TreeNode("Events");
      if (events != null && events.Any())
      {
        foreach (var eventProxy in events)
        {
          var eventNode = new TreeNode(eventProxy.ItemDisplayName);
          eventNode.AddNode(new TreeNode(eventProxy.TimeStamp.ToString()));
          eventNode.AddNode(new TreeNode("Duration", eventProxy.Duration.ToString()));
        }
      }

      return eventsNode;
    }

    public ITreeNode FacetsNode(List<IFacetNodeFactory> customFacetKeyBulletFactories, XConnectClient xConnectClient)
    {
      var toReturn = new TreeNode("Facets");
      var FacetHelper = new FacetHelper(XConnectFacets);

      var FacetTreeHelper = new FacetBranchHelper(customFacetKeyBulletFactories, xConnectClient);


      if (XConnectFacets != null)
      {
        foreach (var targetFacetKey in TargetedFacetKeys)
        {
          toReturn.AddNode(FacetTreeHelper.GetFacetTreeNode(targetFacetKey, FacetHelper));
        }
      }

      return toReturn;
    }

    public ITreeNode IdentifiersNode(List<Sitecore.Analytics.Model.Entities.ContactIdentifier> contactIdentifiers)
    {
      var toReturn = new TreeNode("Identifiers");

      if (contactIdentifiers != null && contactIdentifiers.Any())
      {
        foreach (var contactIdentifier in contactIdentifiers)
        {
          toReturn.AddNode(new TreeNode(contactIdentifier.Source, contactIdentifier.Identifier));
        }
      }

      return toReturn;
    }

    public ITreeNode InteractionsNode(Contact xConnectContact, XConnectClient xConnectClient)
    {
      var toReturn = new TreeNode("Interactions");

      var interactionHelper = new InteractionHelper();

      var knownInteractions = interactionHelper.GetKnownInteractions(xConnectContact, xConnectClient);

      if (knownInteractions != null && knownInteractions.Any())
      {
        foreach (var knownInteraction in knownInteractions)
        {
          var treeNode = new TreeNode(knownInteraction.ChannelName);
          //treeNode.AddNode(new TreeNode("Device Profile",knownInteraction.DeviceProfile))
          treeNode.AddNode(EventsNode(knownInteraction.EventsB));

          treeNode.AddRawNode(knownInteraction.SerializedAsJson);

          toReturn.AddNode(treeNode);
        }
      }

      return toReturn;
    }

    public List<ITreeNode> Tier1NodeBuilder(Sitecore.Analytics.Tracking.Contact trackingContact, XConnectClient xConnectClient, Contact xConnectContact)
    {
      var toReturn = new List<ITreeNode>();


      toReturn.Add(TrackingContactNode(trackingContact, xConnectClient));
      toReturn.Add(IdentifiersNode(trackingContact.Identifiers.ToList()));
      toReturn.Add(FacetsNode(CustomFacetKeyBulletFactories, xConnectClient));
      toReturn.Add(InteractionsNode(xConnectContact, xConnectClient));

      return toReturn;
    }
    public ITreeNode TrackingContactNode(Sitecore.Analytics.Tracking.Contact trackingContact, XConnectClient xConnectClient)
    {
      var toReturn = new TreeNode("Tracking Contact");
      if (trackingContact != null)
      {
        toReturn.AddNode(new TreeNode("Is New", trackingContact.IsNew.ToString()));
        toReturn.AddNode(new TreeNode("Contact Id", trackingContact.ContactId.ToString()));
        toReturn.AddNode(new TreeNode("Identification Level", trackingContact.IdentificationLevel.ToString()));

        var ContractResolver = new XdbJsonContractResolver(xConnectClient.Model, true, true);

        var serializerSettings = new JsonSerializerSettings
        {
          ContractResolver = ContractResolver,
          DateTimeZoneHandling = DateTimeZoneHandling.Utc,
          DefaultValueHandling = DefaultValueHandling.Ignore,
          Formatting = Formatting.Indented
        };

        var serialized = JsonConvert.SerializeObject(trackingContact, serializerSettings);

        toReturn.AddRawNode(serialized);
      }

      return toReturn;
    }
  }
}