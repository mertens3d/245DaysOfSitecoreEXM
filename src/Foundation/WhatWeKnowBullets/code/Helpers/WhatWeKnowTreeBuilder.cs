using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using LearnEXM.Foundation.xConnectHelper.Helpers;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers
{
  public class WhatWeKnowTreeBuilder
  {
    public WhatWeKnowTreeBuilder(List<string> targetedFacetKeys)
    {
      TargetedFacetKeys = targetedFacetKeys;
      xConnectHelper = new XConnectHelper(TargetedFacetKeys);
    }

    private List<string> TargetedFacetKeys { get; set; } = new List<string>();
    private XConnectHelper xConnectHelper { get; }

    public IWhatWeKnowTree GetWhatWeKnowTree(Sitecore.Analytics.Tracking.Contact trackingContact)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) GetWhatWeKnowTree");
      IWhatWeKnowTree toReturn = new Concretions.WhatWeKnowTree("What We Know");

      using (XConnectClient xConnectClient = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
      {
        try
        {
          IdentifiedContactReference IdentifiedContactReference = xConnectHelper.GetIdentifierFromTrackingContact(trackingContact);
          Contact XConnectContact = xConnectHelper.IdentifyKnownContact(IdentifiedContactReference);

          var tier1Nodes = new Tier1Nodes(trackingContact, TargetedFacetKeys);
          toReturn.Root.AddNodes(tier1Nodes.Tier1NodeBuilder(trackingContact, xConnectClient, XConnectContact));
        }
        catch (XdbExecutionException ex)
        {
          Sitecore.Diagnostics.Log.Error(ProjConstants.Logger.Prefix + ex.Message, this);
        }
      }

      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "e) GetWhatWeKnowTree");
      return toReturn;
    }
  }
}