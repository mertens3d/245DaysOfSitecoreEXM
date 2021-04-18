﻿using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.CollectionModel.Builder.Interactions
{
  public abstract class _interactionBase
  {
    public Sitecore.Analytics.Tracking.Contact TrackingContact { get; private set; }
    public Sitecore.Analytics.Model.Entities.ContactIdentifier AnyIdentifier { get; }
    public IdentifiedContactReference IdentifiedContactReference { get; }

    public _interactionBase(string identifier)
    {
      Identifier = identifier;
    }

    protected _interactionBase(Contact xConnectcontact)
    {
      XConnectContact = xConnectcontact;
    }

    protected _interactionBase(Sitecore.Analytics.Tracking.Contact trackingContact)
    {
      TrackingContact = trackingContact;
      AnyIdentifier = Sitecore.Analytics.Tracker.Current.Contact.Identifiers.FirstOrDefault();
      IdentifiedContactReference = new IdentifiedContactReference(AnyIdentifier.Source, AnyIdentifier.Identifier);
    }

    public List<string> Errors { get; set; } = new List<string>();

    public string Identifier { get; set; }

    public Contact XConnectContact { get; set; } = null;

    protected XConnectClient Client { get; set; }

    private Sitecore.Analytics.Tracking.ContactManager Manager { get; set; }
    protected IXConnectFacets XConnectFacets { get; set; }
    public void ExecuteInteraction()
    {
      using (Client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
      {
        try
        {
          IdentifiedContactReference identifiedReference = new IdentifiedContactReference(AnyIdentifier.Source, AnyIdentifier.Identifier);
          //var interactionThing = Guid.Parse("E6067926-1F45-E611-82E6-34E6D7117DCB");

          //Sitecore.XConnect.InteractionReference interactionReference = new InteractionReference(identifiedReference, interactionThing);

          XConnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");


          //var references = new List<InteractionReference>() {
          //  interactionReference
          //};




          var expandOptions = new ExpandOptions(new[]{
              CinemaInfo.DefaultFacetKey,
              CinemaVisitorInfo.DefaultFacetKey,
              EmailAddressList.DefaultFacetKey,
              PersonalInformation.DefaultFacetKey,
          });

          //var interactions = Client.Get<Interaction>(references, expandOptions);


          if (!Sitecore.Analytics.Tracker.Current.Contact.IsNew)
          {
            InteractionBody();
          }
          else
          {
            Sitecore.Diagnostics.Log.Error("Contact is new. Ensure contact exists before executing interaction", this);
          }

          Client.Submit();
          ResetSession();
        }
        catch (XdbExecutionException ex)
        {
          Errors.Add(ex.Message);
        }
      }
    }

    private void ResetSession()
    {
      Manager = Sitecore.Configuration.Factory.CreateObject("tracking/contactManager", true) as Sitecore.Analytics.Tracking.ContactManager;
      Manager.RemoveFromSession(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
      Sitecore.Analytics.Tracker.Current.Session.Contact = Manager.LoadContact(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
    }

    public abstract void InteractionBody();

    //protected IdentifiedContactReference IdentifiedContactReference { get; set; }
    //public KnownData KnownData { get; private set; }
  }
}