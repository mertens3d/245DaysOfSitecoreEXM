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
    public Tier1Nodes(Sitecore.Analytics.Tracking.Contact trackingContact, List<string> targetedFacetKeys, WeKnowTreeOptions options)
    {
      this.XConnectFacets = trackingContact.GetFacet<IXConnectFacets>("XConnectFacets"); ;
      TargetedFacetKeys = targetedFacetKeys;
      TreeOptions = options;
    }

    private List<string> TargetedFacetKeys { get; }
    private WeKnowTreeOptions TreeOptions { get; }
    private IXConnectFacets XConnectFacets { get; set; }

    public IWeKnowTreeNode EventsNode(List<xConnectHelper.Proxies.EventRecordProxy> events)
    {
      WeKnowTreeNode toReturn = null;

      if (events != null && events.Any() && TreeOptions.IncludeEvents)
      {
        toReturn = new WeKnowTreeNode("Events", TreeOptions);
        foreach (var eventProxy in events)
        {
          var eventNode = new WeKnowTreeNode(eventProxy.ItemDisplayName, TreeOptions);
          eventNode.AddNode(new WeKnowTreeNode(eventProxy.TimeStamp.ToString(), TreeOptions));
          eventNode.AddNode(new WeKnowTreeNode("Duration", eventProxy.Duration.ToString(), TreeOptions));
        }
      }

      return toReturn;
    }

    public IWeKnowTreeNode FacetsNode(XConnectClient xConnectClient)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) FacetsNode");
      WeKnowTreeNode toReturn = null;

      if (TreeOptions.IncludeFacets)
      {
        toReturn = new WeKnowTreeNode("Facets", TreeOptions);

        var FacetTreeHelper = new FacetBranchHelper(xConnectClient, XConnectFacets, TreeOptions);

        if (XConnectFacets != null)
        {
          foreach (var targetFacetKey in TargetedFacetKeys)
          {
            toReturn.AddNode(FacetTreeHelper.GetFacetTreeNode(targetFacetKey));
          }
        }

        toReturn.AddNode(FacetTreeHelper.FoundFacetKeys());

        Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "e) FacetsNode");
      }
      return toReturn;
    }

    public IWeKnowTreeNode IdentifiersNode(List<Sitecore.Analytics.Model.Entities.ContactIdentifier> contactIdentifiers)
    {
      WeKnowTreeNode toReturn = null;

      if (contactIdentifiers != null && contactIdentifiers.Any() && TreeOptions.IncludeIdentifiers)
      {
        toReturn = new WeKnowTreeNode("Identifiers", TreeOptions);

        foreach (var contactIdentifier in contactIdentifiers)
        {
          toReturn.AddNode(new WeKnowTreeNode(contactIdentifier.Source, contactIdentifier.Identifier, TreeOptions));
        }
      }

      return toReturn;
    }

    public IWeKnowTreeNode InteractionsNode(Contact xConnectContact, XConnectClient xConnectClient)
    {
      WeKnowTreeNode toReturn = null;

      if (TreeOptions.IncludeInteractions)
      {
        toReturn = new WeKnowTreeNode("Interactions", TreeOptions);
        var interactionHelper = new InteractionHelper();

        var knownInteractions = interactionHelper.GetKnownInteractions(xConnectContact, xConnectClient);

        if (knownInteractions != null && knownInteractions.Any())
        {
          foreach (var knownInteraction in knownInteractions)
          {
            var treeNode = new WeKnowTreeNode(knownInteraction.ChannelName, TreeOptions);

            treeNode.AddNode(EventsNode(knownInteraction.EventsB));

            treeNode.AddRawNode(knownInteraction.SerializedAsJson);

            toReturn.AddNode(treeNode);
          }
        }
      }
      return toReturn;
    }

    public List<IWeKnowTreeNode> Tier1NodeBuilder(Sitecore.Analytics.Tracking.Contact trackingContact, XConnectClient xConnectClient, Contact xConnectContact)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) Tier1NodeBuilder");
      var toReturn = new List<IWeKnowTreeNode>();

      toReturn.Add(TrackingContactNode(trackingContact, xConnectClient));
      toReturn.Add(IdentifiersNode(trackingContact.Identifiers.ToList()));
      toReturn.Add(FacetsNode(xConnectClient));
      toReturn.Add(InteractionsNode(xConnectContact, xConnectClient));

      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) Tier1NodeBuilder");
      return toReturn;
    }

    public IWeKnowTreeNode TrackingContactNode(Sitecore.Analytics.Tracking.Contact trackingContact, XConnectClient xConnectClient)
    {
      WeKnowTreeNode toReturn = null;
      if (trackingContact != null && TreeOptions.IncludeTrackingContact)
      {
        toReturn = new WeKnowTreeNode("Tracking Contact", TreeOptions);

        toReturn.AddNode(new WeKnowTreeNode("Is New", trackingContact.IsNew.ToString(), TreeOptions));
        toReturn.AddNode(new WeKnowTreeNode("Contact Id", trackingContact.ContactId.ToString(), TreeOptions));
        toReturn.AddNode(new WeKnowTreeNode("Identification Level", trackingContact.IdentificationLevel.ToString(), TreeOptions));

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