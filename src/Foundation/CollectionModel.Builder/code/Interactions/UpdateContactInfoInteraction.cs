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

    private FacetHelper FacetHelper { get; set; }
    public ICandidateMockContactInfo CandidateContactInfo { get; }

    public override void InteractionBody()
    {
      FacetHelper = new FacetHelper(XConnectFacets);

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

    private void SetAddressListFacet()
    {

      AddressList addressList = FacetHelper.SafeGetCreateFacet<AddressList>(AddressList.DefaultFacetKey);
      var address = new Address();
      address.City = CandidateContactInfo.AddressCity;
      address.AddressLine1 = CandidateContactInfo.AddressStreet;
      address.CountryCode = CandidateContactInfo.PostalCode;
      address.StateOrProvince = CandidateContactInfo.AddressStateOrProvince;


      addressList.PreferredAddress = address;

      Client.SetFacet<AddressList>(IdentifiedContactReference, AddressList.DefaultFacetKey, addressList);

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

      personalInformation.FirstName = CandidateContactInfo.FirstName;
      personalInformation.LastName = CandidateContactInfo. LastName;

      Client.SetFacet(IdentifiedContactReference, PersonalInformation.DefaultFacetKey, personalInformation);
    }

    private void SetCinemaVisitorInfoFacet()
    {
      CinemaVisitorInfo visitorInfo = new CinemaVisitorInfo()
      {
        FavoriteMovie = CandidateContactInfo.FavoriteMovie
      };

      Client.SetFacet(IdentifiedContactReference, CinemaVisitorInfo.DefaultFacetKey, visitorInfo);
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
  }
}