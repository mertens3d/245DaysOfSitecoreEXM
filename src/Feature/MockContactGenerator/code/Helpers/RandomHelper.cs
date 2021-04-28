using LearnEXM.Foundation.Marketing;
using System;
using System.Collections.Generic;

namespace LearnEXM.Feature.MockContactGenerator.Helpers
{
  public class RandomHelper
  {
    public RandomHelper()
    {
      RandomGen = new Random(DateTime.Now.Millisecond);
    }

    private Random RandomGen { get; set; }

    public int RandomInt(int floorInclusive, int ceiling)
    {
      return RandomGen.Next(floorInclusive, ceiling);
    }

    public int RandomInt(int ceiling)
    {
      return RandomGen.Next(0, ceiling);
    }

    public string RandomListItem(List<string> list)
    {
      var randomIdx = RandomGen.Next(list.Count);
      return list[randomIdx];
    }

    public string RandomPhoneNumber()
    {
      string toReturn = RandomDigit() + RandomDigit() + RandomDigit();
      toReturn = toReturn + RandomDigit() + RandomDigit() + RandomDigit();
      toReturn = toReturn + RandomDigit() + RandomDigit() + RandomDigit() + RandomDigit();
      toReturn = toReturn + (RandomGen.Next(2) == 0 ? "" : " ext " + RandomDigit() + RandomDigit() + RandomDigit() + RandomDigit());
      return toReturn;
    }

    private string RandomDigit()
    {
      return RandomGen.Next(10).ToString();
    }

    internal string RandomGender()
    {
     return RandomInt(2) == 0 ? MarketingConst.XConnect.Genders.Male : MarketingConst.XConnect.Genders.Female;
    }

    internal string FakeEmailAddress(CandidateMockContactInfo mockData)
    {
     return mockData.FirstName + "." + mockData.LastName + "." + mockData.SimpleId + "@mock." + mockData.CompanyName.Replace(" ", string.Empty) + ".com";
    }
  }
}