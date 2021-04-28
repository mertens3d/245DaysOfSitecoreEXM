using LearnEXM.Foundation.Marketing;
using System.Collections.Generic;

namespace LearnEXM.Feature.MockContactGenerator
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
        MarketingConst.ContactType.ContactTypeA,
        MarketingConst.ContactType.ContactTypeB,
        MarketingConst.ContactType.ContactTypeC,
        MarketingConst.ContactType.ContactTypeD,
    };

    public List<string> Divisions = new List<string>{
            MarketingConst.Divisions.DivisionA,
            MarketingConst.Divisions.DivisionB,
            MarketingConst.Divisions.DivisionC,
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

    public List<string> FirstNames = new List<string>
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

    public List<string> LineOfBusiness = new List<string> {
            "Grocery",
            "Retail",
            "Government",
            "Utilities",
            "Machining",
            "Sitecore Web Development"
        };

    public List<string> Movies = new List<string>
    {
      "Pulp Fiction",
      "Reservoir Dogs",
      "Close Encounters of the Third Kind",
      "Mr. Holland's Opus",
      "The Big Chill",
      "Big Fish",
      "Pink Floyd's the Wall",
      "Breakfast Club",
      "Up",
      "Amadeus"
    };
  }
}