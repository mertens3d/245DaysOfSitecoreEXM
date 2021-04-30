using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.LearnEXMRoot.Interfaces;
using LearnEXM.Foundation.xConnectHelper.Helpers;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.Interactions
{
  public class UpdateContactInfoInteraction : _interactionBase
  {
    public UpdateContactInfoInteraction(ICandidateMockContactInfo candidateContactInfo, Sitecore.Analytics.Tracking.Contact trackingContact) : base(trackingContact)
    {
      CandidateContactInfo = candidateContactInfo;
    }

    private FacetEditHelper FacetEditHelper { get; set; }
    public ICandidateMockContactInfo CandidateContactInfo { get; }

    public override void InteractionBody()
    {
      FacetEditHelper = new FacetEditHelper(XConnectFacets);

      if (XConnectContact != null)
      {
        SetPersonalInformationFacet();
        SetCinemaVisitorInfoFacet();
        SetEmailFacet();
        SetCinemaInfoFacet();
        SetAddressListFacet();
      }
      else
      {
        Sitecore.Diagnostics.Log.Error(CollectionConst.Logger.Prefix + "null xConnectContact", this);
      }

      Interaction interaction = new Interaction(IdentifiedContactReference, InteractionInitiator.Brand, CollectionConst.XConnect.Channels.RegisterInteractionCode, string.Empty);

      interaction.Events.Add(new Goal(CollectionConst.XConnect.Goals.RegistrationGoal, DateTime.UtcNow));

      Client.AddInteraction(interaction);
    }

    private void SetEmailFacet()
    {
      var preferredKey = "Work";
      var preferredEmail = new EmailAddress(CandidateContactInfo.EmailAddress, true);

      var emailFacet = new EmailAddressList(preferredEmail, preferredKey)
      {
        Others = new System.Collections.Generic.Dictionary<string, EmailAddress>()
        {
          { "Spam", new EmailAddress("spam@me.com", false) }
        },
      };

      Client.SetFacet(new FacetReference(IdentifiedContactReference, EmailAddressList.DefaultFacetKey), emailFacet);
    }

    private void SetAddressListFacet()
    {
      AddressList addressList = FacetEditHelper.SafeGetFacet<AddressList>(AddressList.DefaultFacetKey);

      if (addressList == null)
      {
        var address = new Address();
        addressList = new AddressList(address, "default");
      }

      if (addressList != null)
      {
        var address = new Address
        {
          AddressLine1 = CandidateContactInfo.AddressStreet1,
          AddressLine2 = CandidateContactInfo.AddressStreet2,
          AddressLine3 = CandidateContactInfo.AddressStreet3,
          AddressLine4 = CandidateContactInfo.AddressStreet4,
          City = CandidateContactInfo.AddressCity,
          CountryCode = CandidateContactInfo.CountryCode,
          GeoCoordinate =  new GeoCoordinate( CandidateContactInfo.GeoCoordinateLatitude, CandidateContactInfo.GeoCoordinateLongitude),//new GeoCoordinate(51.507351f, -0.127758f),
          PostalCode = CandidateContactInfo.PostalCode,
          StateOrProvince = CandidateContactInfo.AddressStateOrProvince,

        };

        addressList.PreferredAddress = address;
        addressList.PreferredKey = CandidateContactInfo.AddressListPreferredKey;

        Client.SetAddresses(IdentifiedContactReference, addressList);
      }
      else
      {
        Sitecore.Diagnostics.Log.Debug("Address List Facet was null");
      }
    }

    private void SetCinemaInfoFacet()
    {
      CinemaInfo cinemaInfo = FacetEditHelper.SafeGetFacet<CinemaInfo>(CinemaInfo.DefaultFacetKey);

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

      personalInformation.FirstName = CandidateContactInfo.FirstName;
      personalInformation.LastName = CandidateContactInfo.LastName;
      if (CandidateContactInfo.Birthdate != null)
      {
        personalInformation.Birthdate = CandidateContactInfo.Birthdate;
      }
      personalInformation.Gender = CandidateContactInfo.Gender;

      Client.SetFacet<PersonalInformation>(new FacetReference(IdentifiedContactReference, PersonalInformation.DefaultFacetKey), personalInformation);
    }

    private void SetCinemaVisitorInfoFacet()
    {
      CinemaVisitorInfo visitorInfo = new CinemaVisitorInfo()
      {
        FavoriteMovie = CandidateContactInfo.FavoriteMovie
      };

      Client.SetFacet(IdentifiedContactReference, CinemaVisitorInfo.DefaultFacetKey, visitorInfo);
    }
  }
}