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

      // address
      toReturn.AddressStateOrProvince = RandomHelper.RandomListItem(SourceLists.AddressState);
      toReturn.AddressStreet = RandomHelper.RandomInt(9999) + " " + RandomHelper.RandomListItem(SourceLists.AddressStreet);
      toReturn.PostalCode = RandomHelper.RandomInt(11111, 99999).ToString();
      toReturn.AddressListPreferredKey = LearnEXM.Foundation.CollectionModel.Builder.CollectionConst.XConnect.AddressListKeys.PreferredAddressKey;


      
      // visitor info
      toReturn.FavoriteMovie = RandomHelper.RandomListItem(SourceLists.Movies);


      //personal
      toReturn.FirstName = RandomHelper.RandomListItem(SourceLists.FirstNames);
      toReturn.LastName = RandomHelper.RandomListItem(SourceLists.LastNames);
      toReturn.Birthdate = new DateTime(RandomHelper.RandomInt(1980, 2010), RandomHelper.RandomInt(1, 12), RandomHelper.RandomInt(1, 28));
      toReturn.Gender = RandomHelper.RandomGender();
      
      

      // business marketing
      toReturn.ContactDivision = RandomHelper.RandomListItem(SourceLists.Divisions);
      toReturn.ContactType = RandomHelper.RandomListItem(SourceLists.ContactType);
      toReturn.CompanyName = RandomHelper.RandomListItem(SourceLists.CompanyName);
      toReturn.ContactLineOfBusiness = RandomHelper.RandomListItem(SourceLists.LineOfBusiness);

      toReturn.ContactPhone = RandomHelper.RandomPhoneNumber();

      toReturn.SimpleId = RandomHelper.RandomInt(10000);
      toReturn.MarketingIdentifierId = "MockContact." + Guid.NewGuid();

      // email
      toReturn.EmailAddress = RandomHelper.FakeEmailAddress(toReturn);

      return toReturn;
    }
  }
}