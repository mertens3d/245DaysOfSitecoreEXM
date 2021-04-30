using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers
{
  public class TrackingHelper
  {
    private List<string> targetFacetsTypes;

    public TrackingHelper(List<string> targetFacetsTypes)
    {
      this.targetFacetsTypes = targetFacetsTypes;
    }

    public IdentifiedContactReference GetIdentifierFromSourceIdentifier(string source, string identifier)
    {
      return new IdentifiedContactReference(source, identifier);
    }

    public IdentifiedContactReference GetIdentifierFromTrackingContact(Sitecore.Analytics.Tracking.Contact trackingContact)
    {
      // moved

      IdentifiedContactReference toReturn = null;

      if (trackingContact != null && trackingContact.IdentificationLevel == Sitecore.Analytics.Model.ContactIdentificationLevel.Known)
      {
        var AnyIdentifier = Tracker.Current.Contact.Identifiers.FirstOrDefault();
        toReturn = GetIdentifierFromSourceIdentifier(AnyIdentifier.Source, AnyIdentifier.Identifier);
      }

      return toReturn;
    }

    private IXConnectFacets XConnectFacets { get; set; }

    //public KnownData GetKnownDataViaTracker(Sitecore.Analytics.Tracking.Contact trackingContact)
    //{
    //  KnownData toReturn = null;

    //  using (XConnectClient xConnectClient = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
    //  {
    //    try
    //    {
    //      toReturn = new KnownData("What We Know");

    //      IdentifiedContactReference IdentifiedContactReference = GetIdentifierFromTrackingContact(trackingContact);
    //      Contact XConnectContact = IdentifyKnownContact(IdentifiedContactReference);

    //      XConnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");

    //      toReturn.WhatWeKnowTree.Root.AddNode(TrackingContactNode(trackingContact, xConnectClient));

    //      toReturn.WhatWeKnowTree.Root.AddNode(IdentifiersNode(Tracker.Current.Contact.Identifiers.ToList()));
    //      toReturn.WhatWeKnowTree.Root.AddNode(FacetsNode());
    //      toReturn.WhatWeKnowTree.Root.AddNode(InteractionsNode(XConnectContact, xConnectClient));
    //    }
    //    catch (XdbExecutionException ex)
    //    {
    //      Sitecore.Diagnostics.Log.Error(ProjConstants.Logger.Prefix + ex.Message, this);
    //    }
    //  }

    //  return toReturn;
    //}

    public Contact IdentifyKnownContact(IdentifiedContactReference identifiedReference)
    {
      Contact toReturn = null;

      if (identifiedReference != null)
      {
        var expandOptions = new ContactExpandOptions(targetFacetsTypes.ToArray())
        {
          Interactions = new RelatedInteractionsExpandOptions()
          {
            StartDateTime = DateTime.MinValue,
            Limit = int.MaxValue
          }
        };

        using (var Client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
        {
          try
          {
            toReturn = Client.Get(identifiedReference, expandOptions);
          }
          catch (XdbExecutionException ex)
          {
            Sitecore.Diagnostics.Log.Error(ProjConstants.Logger.Prefix, ex, this);
          }
        }
      }
      else
      {
        Sitecore.Diagnostics.Log.Error(ProjConstants.Logger.Prefix + "Identified Reference was null", this);
      }

      return toReturn;
    }
  }
}