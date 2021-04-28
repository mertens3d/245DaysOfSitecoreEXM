using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.Marketing;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using System;

namespace LearnEXM.Feature.MockContactGenerator.Helpers
{
  internal class OperationsDataHelper
  {
    private CandidateMockContactInfo candidate;

    public OperationsDataHelper(CandidateMockContactInfo candidate)
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
      var emailAddress = new EmailAddress(candidate.EmailAddress, true);
      var emailAddressList = new EmailAddressList(emailAddress, MarketingConst.XConnect.EmailKeys.Work);

      return emailAddressList;
    }

    public CinemaBusinessMarketing MakeFacetMarketing()
    {
      var Marketing = new CinemaBusinessMarketing();
      Marketing.CompanyName = candidate.CompanyName;
      Marketing.ContactDivision = candidate.ContactDivision;
      Marketing.ContactLineOfBusiness = candidate.ContactLineOfBusiness;
      Marketing.ContactPhone = candidate.ContactPhone;
      Marketing.ContactType = candidate.ContactType;

      return Marketing;
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