using Shared.Models.SitecoreCinema;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Linq;

namespace Shared.XConnect.Interactions
{
  public class UpdateContactInfoInteraction : _interactionBase
  {
    public UpdateContactInfoInteraction(string firstName, string lastName, string favoriteMovie, string sitecoreCinemaIdentifier, Sitecore.Analytics.Tracking.Contact trackingContact) : base(trackingContact)
    {
      FirstName = firstName;
      LastName = lastName;
      FavoriteMovie = favoriteMovie;
      SitecoreCinemaIdentifier = sitecoreCinemaIdentifier;
    }

    private string FirstName { get; }
    private string LastName { get; }
    private string FavoriteMovie { get; }
    public string SitecoreCinemaIdentifier { get; set; }

    public override void InteractionBody() { }
    public void UpdateData()
    {
      var manager = Sitecore.Configuration.Factory.CreateObject("tracking/contactManager", true) as Sitecore.Analytics.Tracking.ContactManager;

      if (!Sitecore.Analytics.Tracker.Current.Contact.IsNew)
      {
        var anyIdentifier = Sitecore.Analytics.Tracker.Current.Contact.Identifiers.FirstOrDefault();

        using (XConnectClient xConnectClient = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
        {
          try
          {
            var xConnectContact = xConnectClient.Get<Contact>(new IdentifiedContactReference(anyIdentifier.Source, anyIdentifier.Identifier), new Sitecore.XConnect.ExpandOptions(PersonalInformation.DefaultFacetKey));
            PersonalInformation personalInformation = null;

            if (xConnectContact != null)
            {
              if (xConnectContact.Personal() != null)
              {
                personalInformation = xConnectContact.Personal();
              }
              else
              {
                personalInformation = new PersonalInformation();
              }
            }

            personalInformation.FirstName = this.FirstName;
            personalInformation.LastName = this.LastName;

            xConnectClient.SetFacet<PersonalInformation>(xConnectContact, PersonalInformation.DefaultFacetKey, personalInformation);

            CinemaVisitorInfo visitorInfo = new CinemaVisitorInfo()
            {
              FavoriteMovie = this.FavoriteMovie
            };

            Client.SetFacet<CinemaVisitorInfo>(XConnectContact, CinemaVisitorInfo.DefaultFacetKey, visitorInfo);

            Interaction interaction = new Interaction(XConnectContact, InteractionInitiator.Brand, Const.XConnect.Channels.RegisterInteractionCode, string.Empty);

            interaction.Events.Add(new Goal(Const.XConnect.Goals.RegistrationGoal, DateTime.UtcNow));

            Client.AddInteraction(interaction);
            xConnectClient.Submit();

            manager.RemoveFromSession(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
            Sitecore.Analytics.Tracker.Current.Session.Contact = manager.LoadContact(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
          }
          catch (Exception ex)
          {
            Sitecore.Diagnostics.Log.Error(ex.Message, this);
          }
        }
      }
    }
  }
}