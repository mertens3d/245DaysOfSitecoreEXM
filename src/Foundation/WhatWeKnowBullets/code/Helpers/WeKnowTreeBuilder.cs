using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using LearnEXM.Foundation.xConnectHelper.Helpers;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers
{
  public class WeKnowTreeBuilder
  {
    public WeKnowTreeBuilder( WeKnowTreeOptions options)
    {
      xConnectHelper = new XConnectHelper(options.TargetedFacetKeys);
      Options = options;
    }

    private XConnectHelper xConnectHelper { get; }
    private WeKnowTreeOptions Options { get; }

    public IWeKnowTree GetWhatWeKnowTreeFromXConnectContact(Sitecore.XConnect.Contact XConnectContact)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) GetWhatWeKnowTreeFromXConnectContact");
      
      IWeKnowTree toReturn = new Concretions.WhatWeKnowTree("What We Know", Options);

      using (XConnectClient xConnectClient = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
      {
        try
        {
          var tier1Nodes = new Tier1Nodes(XConnectContact, Options);
          toReturn.Root.AddNodes(tier1Nodes.Tier1NodeBuilder(null, xConnectClient, XConnectContact));
        }
        catch (XdbExecutionException ex)
        {
          Sitecore.Diagnostics.Log.Error(ProjConstants.Logger.Prefix + ex.Message, this);
        }
      }

      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "e) GetWhatWeKnowTreeFromXConnectContact");
      return toReturn;
    }
    public IWeKnowTree GetWeKnowTreeFromTrackingContact(Sitecore.Analytics.Tracking.Contact trackingContact)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) GetWhatWeKnowTree");
      IWeKnowTree toReturn = new Concretions.WhatWeKnowTree("What We Know", Options);

      using (XConnectClient xConnectClient = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
      {
        try
        {
          IdentifiedContactReference IdentifiedContactReference = xConnectHelper.GetIdentifierFromTrackingContact(trackingContact);
          Contact XConnectContact = xConnectHelper.IdentifyKnownContact(IdentifiedContactReference);

          var tier1Nodes = new Tier1Nodes(trackingContact,  Options);
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