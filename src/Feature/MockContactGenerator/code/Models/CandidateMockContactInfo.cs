using LearnEXM.Foundation.LearnEXMRoot.Interfaces;
using System;

namespace LearnEXM.Feature.MockContactGenerator
{
  public class CandidateMockContactInfo : ICandidateMockContactInfo
  {
    public string AddressCity { get; set; }
    public string AddressListPreferredKey { get; set; }
    public string AddressStateOrProvince { get; set; }
    public string AddressStreet1 { get; set; }
    public string AddressStreet2 { get; set; }
    public string AddressStreet3 { get; set; }
    public string AddressStreet4 { get; set; }
    public DateTime? Birthdate { get; set; }
    public string CompanyName { get; set; }
    public string ContactDivision { get; set; }
    public string ContactLineOfBusiness { get; set; }
    public string ContactPhone { get; set; }
    public string ContactType { get; set; }
    public string CountryCode { get; set; }
    public string EmailAddress { get; set; }
    public string FavoriteMovie { get; set; }
    public string FirstName { get; set; }
    public string Gender { get; set; }
    public double GeoCoordinateLatitude { get; set; }
    public double GeoCoordinateLongitude { get; set; }
    public string LastName { get; set; }
    public string MarketingIdentifierId { get; set; }
    public string PostalCode { get; set; }
    public int SimpleId { get; set; }
    public string SitecoreCinemaIdentifier { get; set; }
  }
}