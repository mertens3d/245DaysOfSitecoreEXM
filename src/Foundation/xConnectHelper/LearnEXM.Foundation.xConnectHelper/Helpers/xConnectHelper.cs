using Sitecore.Analytics;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.xConnectHelper.Helpers
{
  public class XConnectHelper
  {
    public XConnectHelper(List<string> allFacetKeys)
    {
      if (allFacetKeys != null)
      {
        this.AllFacetKeys = allFacetKeys.ToArray();
      }
    }

    public string[] AllFacetKeys { get; set; } = new string[0];
    public IdentifiedContactReference GetIdentifierFromSourceIdentifier(string source, string identifier)
    {
      return new IdentifiedContactReference(source, identifier);
    }

    public Contact IdentifyKnownContact(IdentifiedContactReference identifiedReference)
    {
      Contact toReturn = null;

      if (identifiedReference != null)
      {
        var expandOptions = new ContactExpandOptions(AllFacetKeys)
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
            Sitecore.Diagnostics.Log.Error(Constants.Logger.LoggingPrefix, ex, this);
          }
        }
      }
      else
      {
        Sitecore.Diagnostics.Log.Error(Constants.Logger.LoggingPrefix + "Identified Reference was null", this);
      }

      return toReturn;
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

  }
}