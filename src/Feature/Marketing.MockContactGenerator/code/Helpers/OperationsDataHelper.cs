using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using Marketing.MockContactGenerator.Models;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using System;

namespace Marketing.MockContactGenerator.Helpers
{
  internal class OperationsDataHelper
  {
    private CandidateContactInfo candidate;

    public OperationsDataHelper(CandidateContactInfo candidate)
    {
      this.candidate = candidate;
    }

    public AddressList MakeFacetAddress()
    {
      var address = new Address();
      address.City = candidate.AddressCity;
      address.AddressLine1 = candidate.AddressStreet;
      address.CountryCode = MarketingConst.XConnect.PostalCodes.US;
      address.PostalCode = candidate.PostalCode;
      address.StateOrProvince = candidate.AddressStateOrProvince;

      var addressist = new AddressList(address, MarketingConst.XConnect.AddressListKeys.Marketing);

      return addressist;
    }

    public EmailAddressList MakeFacetEmailAddressList()
    {
      var emailAddress = new EmailAddress(candidate.Email, true);
      var emailAddressList = new EmailAddressList(emailAddress, MarketingConst.XConnect.EmailKeys.Work);

      return emailAddressList;
    }

    public CinemaVisitorInfo MakeFacetCinemaVisitorInfo()
    {
      var facet = new CinemaVisitorInfo();
      //facet.CompanyName = candidate.CompanyName;
      //facet.ContactDivision = candidate.ContactDivision;
      //facet.ContactLineOfBusiness = candidate.ContactLineOfBusiness;
      //facet.ContactPhone = candidate.ContactPhone;
      //facet.ContactType = candidate.ContactType;

      return facet;
    }

    public PersonalInformation MakeFacetPersonalInformation()
    {
      PersonalInformation personalInformation = new PersonalInformation
      {
        FirstName = candidate.FirstName,
        LastName = candidate.LastName,
        Birthdate = DateTime.Now,
        Gender = candidate.Gender
      };

      return personalInformation;
    }

    public Interaction SetRegistrationGoalInteraction(Contact contact)
    {
      Interaction interaction = new Interaction(contact, InteractionInitiator.Brand, MarketingConst.XConnect.Channels.MockContactRegistration, string.Empty);
      interaction.Events.Add(new Goal(MarketingConst.XConnect.Goals.MockContactRegistered, DateTime.UtcNow));
      return interaction;
    }

    internal Contact CreateContact()
    {
      ContactIdentifier identifier = new ContactIdentifier(MarketingConst.XConnect.ContactIdentifiers.Sources.Marketing, candidate.MarketingIdentifierId, ContactIdentifierType.Known);

      var contact = new Contact(new ContactIdentifier[] { identifier });

      return contact;
    }
  }
}