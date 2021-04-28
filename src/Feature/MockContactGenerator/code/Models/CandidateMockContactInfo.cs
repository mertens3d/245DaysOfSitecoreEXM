using LearnEXM.Foundation.LearnEXMRoot.Interfaces;

namespace LearnEXM.Feature.MockContactGenerator
{
  public class CandidateMockContactInfo : ICandidateMockContactInfo
  {
    public string AddressCity { get; set; }
    public string AddressStateOrProvince { get; set; }
    public string AddressStreet { get; set; }
    public string CompanyName { get; set; }
    public string ContactDivision { get; set; }
    public string ContactLineOfBusiness { get; set; }
    public string ContactPhone { get; set; }
    public string ContactType { get; set; }
    public string EmailAddress { get; set; }
    public string FavoriteMovie { get; set; }
    public string FirstName { get; set; }
    public string Gender { get; set; }
    //public int Id { get; internal set; }
    public string LastName { get; set; }
    public string MarketingIdentifierId { get; set; }
    public string PostalCode { get; set; }
    public int SimpleId { get; set; }
    public string SitecoreCinemaIdentifier { get; set; }
  }
}