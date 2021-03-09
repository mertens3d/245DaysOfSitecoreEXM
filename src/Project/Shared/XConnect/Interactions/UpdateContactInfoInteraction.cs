using Shared.Models.SitecoreCinema;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using System;

namespace Shared.XConnect.Interactions
{
  public class UpdateContactInfoInteraction : _interactionBase
  {
    public UpdateContactInfoInteraction(string firstName, string lastName, string favoriteMovie, string emailAddress, string sitecoreCinemaIdentifier, Sitecore.Analytics.Tracking.Contact trackingContact) : base(trackingContact)
    {
      FirstName = firstName;
      LastName = lastName;
      FavoriteMovie = favoriteMovie;
      EmailAddress = emailAddress;
      SitecoreCinemaIdentifier = sitecoreCinemaIdentifier;
    }

    private string FirstName { get; }
    private string LastName { get; }
    private string FavoriteMovie { get; }
    public string EmailAddress { get; }
    public string SitecoreCinemaIdentifier { get; set; }
    private IXConnectFacets XConnectFacets { get; set; }

    public override void InteractionBody()
    {
      if (XConnectContact != null)
      {
        //IContactEmailAddresses addressesFacet = Tracker.Current.Contact.GetFacet<IContactEmailAddresses>("Emails");
        XConnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");

        SetPersonalInformationFacet();
        SetCinemaVisitorInfoFacet();
        SetEmailFacet();
      }

      Interaction interaction = new Interaction(XConnectContact, InteractionInitiator.Brand, Const.XConnect.Channels.RegisterInteractionCode, string.Empty);

      interaction.Events.Add(new Goal(Const.XConnect.Goals.RegistrationGoal, DateTime.UtcNow));

      Client.AddInteraction(interaction);
    }

    private void SetPersonalInformationFacet()
    {
      PersonalInformation personalInformation = null;

      if (XConnectContact.Personal() != null)
      {
        personalInformation = XConnectContact.Personal();
      }
      else
      {
        personalInformation = new PersonalInformation();
      }

      personalInformation.FirstName = this.FirstName;
      personalInformation.LastName = this.LastName;

      Client.SetFacet<PersonalInformation>(XConnectContact, PersonalInformation.DefaultFacetKey, personalInformation);
    }

    private void SetCinemaVisitorInfoFacet()
    {
      CinemaVisitorInfo visitorInfo = new CinemaVisitorInfo()
      {
        FavoriteMovie = this.FavoriteMovie
      };

      Client.SetFacet<CinemaVisitorInfo>(XConnectContact, CinemaVisitorInfo.DefaultFacetKey, visitorInfo);
    }

    private void SetEmailFacet()
    {
      var preferredKey = "Work";
      var preferredEmail = new EmailAddress(EmailAddress, true);

      EmailAddressList emailAddressListFacet = XConnectFacets.Facets[EmailAddressList.DefaultFacetKey] as EmailAddressList;

      var emailFacet = new EmailAddressList(preferredEmail, preferredKey)
      {
        Others = new System.Collections.Generic.Dictionary<string, EmailAddress>()
        {
          { "Spam", new EmailAddress("spam@me.com", false) }
        },
      };

      Client.SetFacet<EmailAddressList>(new FacetReference(XConnectContact, EmailAddressList.DefaultFacetKey), emailFacet);
    }
  }
}