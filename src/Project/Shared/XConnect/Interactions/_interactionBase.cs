using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.XConnect
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

      //Identifier = xConnectcontact.Identifiers.
    }

    protected _interactionBase(Sitecore.Analytics.Tracking.Contact trackingContact)
    {
      this.TrackingContact = trackingContact;
      AnyIdentifier = Sitecore.Analytics.Tracker.Current.Contact.Identifiers.FirstOrDefault();
      IdentifiedContactReference = new IdentifiedContactReference(AnyIdentifier.Source, AnyIdentifier.Identifier);
      
    }

    public List<string> Errors { get; set; } = new List<string>();

    public string Identifier { get; set; }

    public Contact XConnectContact { get; set; } = null;

    protected XConnectClient Client { get; set; }

    public Sitecore.Analytics.Tracking.ContactManager Manager { get; set; }

    public void ExecuteInteraction()
    {
      using (Client =  Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
      {
        try
        {
          //var clientHelper = new XConnectClientHelper(Client);

          //if (!string.IsNullOrEmpty(Identifier))
          //{
          //  XConnectContact = await clientHelper.GetXConnectContactByIdentifierAsync(Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, Identifier);
          //}

          XConnectContact = Client.Get<Contact>(new IdentifiedContactReference(AnyIdentifier.Source, AnyIdentifier.Identifier), new Sitecore.XConnect.ExpandOptions(PersonalInformation.DefaultFacetKey));
          Manager = Sitecore.Configuration.Factory.CreateObject("tracking/contactManager", true) as Sitecore.Analytics.Tracking.ContactManager;

          if (!Sitecore.Analytics.Tracker.Current.Contact.IsNew)
          {
            InteractionBody();
          }
          else
          {
            Sitecore.Diagnostics.Log.Error("Contact is new. Ensure contact exists before executing interaction", this);
          }


          Client.Submit();

          Manager.RemoveFromSession(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
          Sitecore.Analytics.Tracker.Current.Session.Contact = Manager.LoadContact(Sitecore.Analytics.Tracker.Current.Contact.ContactId);


        }
        catch (XdbExecutionException ex)
        {
          Errors.Add(ex.Message);
        }
      }
    }

    public abstract void InteractionBody();

    //protected IdentifiedContactReference IdentifiedContactReference { get; set; }
    //public KnownData KnownData { get; private set; }
  }
}