using System;

namespace LearnEXM.Feature.MockContactGenerator.Helpers
{
  public class MockContactGeneratorHelper
  {
    public MockContactGeneratorHelper()
    {
      RandomHelper = new RandomHelper();

      SourceLists = new SourceLists();
    }

    public RandomHelper RandomHelper { get; }

    public SourceLists SourceLists { get; private set; }

    public CandidateMockContactInfo GenerateContact()
    {
      CandidateMockContactInfo toReturn = new CandidateMockContactInfo();

      toReturn.AddressStateOrProvince = RandomHelper.RandomListItem(SourceLists.AddressState);
      toReturn.AddressStreet = RandomHelper.RandomInt(9999) + " " + RandomHelper.RandomListItem(SourceLists.AddressStreet);
      toReturn.PostalCode = RandomHelper.RandomInt(11111, 99999).ToString();

      toReturn.CompanyName = RandomHelper.RandomListItem(SourceLists.CompanyName);
      toReturn.ContactType = RandomHelper.RandomListItem(SourceLists.ContactType);

      toReturn.ContactDivision = RandomHelper.RandomListItem(SourceLists.Divisions);
      toReturn.FirstName = RandomHelper.RandomListItem(SourceLists.FirstNames);
      toReturn.LastName = RandomHelper.RandomListItem(SourceLists.LastNames);
      toReturn.ContactLineOfBusiness = RandomHelper.RandomListItem(SourceLists.LineOfBusiness);

      toReturn.ContactPhone = RandomHelper.RandomPhoneNumber();

      toReturn.SimpleId = RandomHelper.RandomInt(10000);
      toReturn.MarketingIdentifierId = "MockContact." + Guid.NewGuid();
      toReturn.Gender = RandomHelper.RandomGender();

      toReturn.FavoriteMovie = RandomHelper.RandomListItem(SourceLists.Movies);

      toReturn.EmailAddress = RandomHelper.FakeEmailAddress(toReturn);

      return toReturn;
    }
  }
}