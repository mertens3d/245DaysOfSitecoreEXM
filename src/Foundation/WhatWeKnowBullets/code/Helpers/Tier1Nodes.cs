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
    public IWhatWeKnowTreeNode EventsNode(List<xConnectHelper.Proxies.EventRecordProxy> events)
    {
      var eventsNode = new WhatWeKnowTreeNode("Events");
      if (events != null && events.Any())
      {
        foreach (var eventProxy in events)
        {
          var eventNode = new WhatWeKnowTreeNode(eventProxy.ItemDisplayName);
          eventNode.AddNode(new WhatWeKnowTreeNode(eventProxy.TimeStamp.ToString()));
          eventNode.AddNode(new WhatWeKnowTreeNode("Duration", eventProxy.Duration.ToString()));
        }
      }

      return eventsNode;
    }

    public IWhatWeKnowTreeNode FacetsNode(List<IFacetNodeFactory> customFacetKeyBulletFactories, XConnectClient xConnectClient)
    {
      var toReturn = new WhatWeKnowTreeNode("Facets");

      var FacetTreeHelper = new FacetBranchHelper(customFacetKeyBulletFactories, xConnectClient, XConnectFacets);


      if (XConnectFacets != null)
      {
        foreach (var targetFacetKey in TargetedFacetKeys)
        {
          toReturn.AddNode(FacetTreeHelper.GetFacetTreeNode(targetFacetKey));
        }
      }

      toReturn.AddNode(FacetTreeHelper.FoundFacetKeys());

      return toReturn;
    }

    public IWhatWeKnowTreeNode IdentifiersNode(List<Sitecore.Analytics.Model.Entities.ContactIdentifier> contactIdentifiers)
    {
      var toReturn = new WhatWeKnowTreeNode("Identifiers");

      if (contactIdentifiers != null && contactIdentifiers.Any())
      {
        foreach (var contactIdentifier in contactIdentifiers)
        {
          toReturn.AddNode(new WhatWeKnowTreeNode(contactIdentifier.Source, contactIdentifier.Identifier));
        }
      }

      return toReturn;
    }

    public IWhatWeKnowTreeNode InteractionsNode(Contact xConnectContact, XConnectClient xConnectClient)
    {
      var toReturn = new WhatWeKnowTreeNode("Interactions");

      var interactionHelper = new InteractionHelper();

      var knownInteractions = interactionHelper.GetKnownInteractions(xConnectContact, xConnectClient);

      if (knownInteractions != null && knownInteractions.Any())
      {
        foreach (var knownInteraction in knownInteractions)
        {
          var treeNode = new WhatWeKnowTreeNode(knownInteraction.ChannelName);
          //treeNode.AddNode(new TreeNode("Device Profile",knownInteraction.DeviceProfile))
          treeNode.AddNode(EventsNode(knownInteraction.EventsB));

          treeNode.AddRawNode(knownInteraction.SerializedAsJson);

          toReturn.AddNode(treeNode);
        }
      }

      return toReturn;
    }

    public List<IWhatWeKnowTreeNode> Tier1NodeBuilder(Sitecore.Analytics.Tracking.Contact trackingContact, XConnectClient xConnectClient, Contact xConnectContact)
    {
      var toReturn = new List<IWhatWeKnowTreeNode>();


      toReturn.Add(TrackingContactNode(trackingContact, xConnectClient));
      toReturn.Add(IdentifiersNode(trackingContact.Identifiers.ToList()));
      toReturn.Add(FacetsNode(CustomFacetKeyBulletFactories, xConnectClient));
      toReturn.Add(InteractionsNode(xConnectContact, xConnectClient));

      return toReturn;
    }
    public IWhatWeKnowTreeNode TrackingContactNode(Sitecore.Analytics.Tracking.Contact trackingContact, XConnectClient xConnectClient)
    {
      var toReturn = new WhatWeKnowTreeNode("Tracking Contact");
      if (trackingContact != null)
      {
        toReturn.AddNode(new WhatWeKnowTreeNode("Is New", trackingContact.IsNew.ToString()));
        toReturn.AddNode(new WhatWeKnowTreeNode("Contact Id", trackingContact.ContactId.ToString()));
        toReturn.AddNode(new WhatWeKnowTreeNode("Identification Level", trackingContact.IdentificationLevel.ToString()));

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