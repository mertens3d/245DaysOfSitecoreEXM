using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using LearnEXM.Foundation.xConnectHelper.Helpers;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Helpers
{
  public class KnownDataHelper
  {
    public KnownDataHelper(List<string> targetedFacetKeys, List<IFacetNodeFactory> customFacetKeyBulletFactories)
    {
      TargetedFacetKeys = targetedFacetKeys;
      CustomFacetKeyBulletFactories = customFacetKeyBulletFactories;
      xConnectHelper = new XConnectHelper(TargetedFacetKeys);
    }

    private List<string> TargetedFacetKeys { get; set; } = new List<string>();
    private List<IFacetNodeFactory> CustomFacetKeyBulletFactories { get; }
    private XConnectHelper xConnectHelper { get; }
    private IXConnectFacets XConnectFacets { get; set; }
    public FacetHelper FacetHelper { get; private set; }
    public FacetTreeHelper FacetTreeHelper { get; private set; }
    private InteractionHelper InteractionHelper { get; set; }

    public KnownData GetKnownDataViaTracker(Sitecore.Analytics.Tracking.Contact trackingContact)
    {
      KnownData toReturn = null;

      using (XConnectClient xConnectClient = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
      {
        try
        {
          toReturn = new KnownData("What We Know");

          IdentifiedContactReference IdentifiedContactReference = xConnectHelper.GetIdentifierFromTrackingContact(trackingContact);
          Contact XConnectContact = xConnectHelper.IdentifyKnownContact(IdentifiedContactReference);

          XConnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");

          FacetHelper = new FacetHelper(XConnectFacets);
          FacetTreeHelper = new FacetTreeHelper(CustomFacetKeyBulletFactories, xConnectClient);
          InteractionHelper = new InteractionHelper();

          toReturn.WhatWeKnowTree.Root.Leaves.Add(IdentifiersNode(Tracker.Current.Contact.Identifiers.ToList()));
          toReturn.WhatWeKnowTree.Root.Leaves.Add(FacetsNode());
          toReturn.WhatWeKnowTree.Root.Leaves.Add(InteractionsNode(XConnectContact, xConnectClient));
        }
        catch (XdbExecutionException ex)
        {
          Sitecore.Diagnostics.Log.Error(WhatWeKnowBulletsConstants.Logger.Prefix + ex.Message, this);
        }
      }

      return toReturn;
    }

    private ITreeNode EventsNode(List<xConnectHelper.Proxies.EventRecordProxy> events)
    {
      var eventsNode = new TreeNode("Events");
      if (events != null && events.Any())
      {
        foreach (var eventProxy in events)
        {
          var eventNode = new TreeNode(eventProxy.ItemDisplayName);
          eventNode.Leaves.Add(new TreeNode(eventProxy.TimeStamp.ToString()));
          eventNode.Leaves.Add(new TreeNode("Duration", eventProxy.Duration.ToString()));
        }
      }

      return eventsNode;
    }

    private ITreeNode InteractionsNode(Contact xConnectContact, XConnectClient xConnectClient)
    {
      var toReturn = new TreeNode("Interactions");

      var knownInteractions = InteractionHelper.GetKnownInteractions(xConnectContact, xConnectClient);

      if (knownInteractions != null && knownInteractions.Any())
      {
        foreach (var knownInteraction in knownInteractions)
        {
          var treeNode = new TreeNode(knownInteraction.ChannelName);
          //treeNode.Leaves.Add(new TreeNode("Device Profile",knownInteraction.DeviceProfile))
          treeNode.Leaves.Add(EventsNode(knownInteraction.EventsB));
          
            
          var rawNode = new TreeNode("raw");
          rawNode.Leaves.Add(new TreeNode(knownInteraction.SerializedAsJson)
          {
            ValueIsJson = true
          });
          treeNode.Leaves.Add(rawNode);


          toReturn.Leaves.Add(treeNode);
        }
      }

      return toReturn;
    }

    private ITreeNode IdentifiersNode(List<Sitecore.Analytics.Model.Entities.ContactIdentifier> contactIdentifiers)
    {
      var toReturn = new TreeNode("Identifiers");

      if (contactIdentifiers != null && contactIdentifiers.Any())
      {
        foreach (var contactIdentifier in contactIdentifiers)
        {
          toReturn.Leaves.Add(new TreeNode(contactIdentifier.Source, contactIdentifier.Identifier));
        }
      }

      return toReturn;
    }

    private ITreeNode FacetsNode()
    {
      var toReturn = new TreeNode("Facets");

      if (XConnectFacets != null)
      {
        foreach (var targetFacetKey in TargetedFacetKeys)
        {
          toReturn.Leaves.Add(FacetTreeHelper.GetFacetTreeNode(targetFacetKey, FacetHelper));
        }
      }

      return toReturn;
    }
  }
}