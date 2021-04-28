using System;

namespace LearnEXM.Foundation.LearnEXMRoot.Interfaces
{
  public interface ICandidateMockContactInfo
  {
    string AddressCity { get; set; }
    string AddressStateOrProvince { get; set; }
    string AddressStreet { get; set; }
    string CompanyName { get; set; }
    string ContactDivision { get; set; }
    string ContactLineOfBusiness { get; set; }
    string ContactPhone { get; set; }
    string ContactType { get; set; }
    string EmailAddress { get; set; }
    string FavoriteMovie { get; set; }
    string FirstName { get; set; }
    string Gender { get; set; }
    //int Id { get; }
    string LastName { get; set; }
    string MarketingIdentifierId { get; }
    string PostalCode { get; set; }
    int SimpleId { get; set; }
    string SitecoreCinemaIdentifier { get; set; }
    string AddressListPreferredKey { get; set; }
    DateTime? Birthdate { get; set; }
  }
}