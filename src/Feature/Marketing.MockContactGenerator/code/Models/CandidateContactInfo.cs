using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketing.MockContactGenerator.Models
{
  public class SourceLists
  {
    public List<string> AddressState = new List<string>
        {
            "Tx",
            "Wi",
            "Ma",
            "Wa",
            "Mi"
        };

    public List<string> AddressStreet = new List<string>
        {
            "Brookhollow",
            "Everwood Lane",
            "Windy Path",
            "Craggy Creek"
        };

    public List<string> CompanyName = new List<string>
        {
            "Red inc.",
            "Blue llc.",
            "Green co.",
            "Pink co.",
            "Yellow inc."
        };

    public List<string> ContactType = new List<string>
    {
        MarketingConst.ContactType.Broker,
        MarketingConst.ContactType.RiskManager,
        MarketingConst.ContactType.Investor,
        MarketingConst.ContactType.Media,
    };

    public List<string> Divisions = new List<string>{
            MarketingConst.Divisions.Insurance,
            MarketingConst.Divisions.Reinsurance,
            MarketingConst.Divisions.SIG,
        };

    public List<string> FirstNames = new List<string>()
    {
        "Bob",
        "Susan",
        "Gregory",
        "Pete",
        "Scooby",
        "Marvin" ,
        "Wendy",
        "Donna",
        "Fred",
        "Nick"
    };

    public List<string> LastNames = new List<string>
                               {
                                "Schwartsburg",
                                "Anthony",
                                "Einstein",
                                "Clark",
                                "Reed",
                                "Milkman",
                                "RedFace",
                                "Green",
                                "Star",
                                "Moonbeam"
                            };

    public List<string> LineOfBusiness = new List<string> {
            "Grocery",
            "Retail",
            "Government",
            "Utilities",
            "Machining",
            "Sitecore Web Development"
        };
  }
  internal class CandidateContactInfo
  {
    public string AddressCity { get; set; }
    public string AddressStateOrProvince { get; set; }
    public string AddressStreet { get; set; }
    public string CompanyName { get; set; }
    public string ContactDivision { get; set; }
    public string ContactLineOfBusiness { get; set; }
    public string ContactPhone { get; set; }
    public string ContactType { get; set; }
    public string Email { get; set; }
    public string MarketingIdentifierId { get; internal set; }
    public string FirstName { get; set; }
    public string Gender { get; set; }
    public string LastName { get; set; }
    public string PostalCode { get; set; }
    public int SimpleId { get; set; }
  }
}
