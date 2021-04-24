using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.xConnectHelper.Helpers;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.Interactions
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
    private FacetHelper FacetHelper { get; set; }

    public override void InteractionBody()
    {
      FacetHelper = new FacetHelper(XConnectFacets);

      if (XConnectContact != null)
      {
        SetPersonalInformationFacet();
        SetCinemaVisitorInfoFacet();
        SetEmailFacet();
        SetCinemaInfoFacet();
      }
      else
      {
        Sitecore.Diagnostics.Log.Error(CollectionConst.Logger.Prefix + "null xConnectContact", this);
      }


      Interaction interaction = new Interaction(IdentifiedContactReference, InteractionInitiator.Brand, CollectionConst.XConnect.Channels.RegisterInteractionCode, string.Empty);

      interaction.Events.Add(new Goal(CollectionConst.XConnect.Goals.RegistrationGoal, DateTime.UtcNow));

      Client.AddInteraction(interaction);
    }

    private void SetCinemaInfoFacet()
    {
      
      CinemaInfo cinemaInfo = FacetHelper.SafeGetCreateFacet<CinemaInfo>(CinemaInfo.DefaultFacetKey);

      if (cinemaInfo != null)
      {
        cinemaInfo.CinimaId = 33;
      }
      else
      {
        cinemaInfo = new CinemaInfo
        {
          CinimaId = 22
        };
      }
      Client.SetFacet<CinemaInfo>(IdentifiedContactReference, CinemaInfo.DefaultFacetKey, cinemaInfo);
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

      personalInformation.FirstName = FirstName;
      personalInformation.LastName = LastName;

      Client.SetFacet(IdentifiedContactReference, PersonalInformation.DefaultFacetKey, personalInformation);
    }

    private void SetCinemaVisitorInfoFacet()
    {
      CinemaVisitorInfo visitorInfo = new CinemaVisitorInfo()
      {
        FavoriteMovie = FavoriteMovie
      };

      Client.SetFacet(IdentifiedContactReference, CinemaVisitorInfo.DefaultFacetKey, visitorInfo);
    }

    private void SetEmailFacet()
    {
      var preferredKey = "Work";
      var preferredEmail = new EmailAddress(EmailAddress, true);

      var emailFacet = new EmailAddressList(preferredEmail, preferredKey)
      {
        Others = new System.Collections.Generic.Dictionary<string, EmailAddress>()
        {
          { "Spam", new EmailAddress("spam@me.com", false) }
        },
      };

      Client.SetFacet(new FacetReference(IdentifiedContactReference, EmailAddressList.DefaultFacetKey), emailFacet);
    }
  }
}