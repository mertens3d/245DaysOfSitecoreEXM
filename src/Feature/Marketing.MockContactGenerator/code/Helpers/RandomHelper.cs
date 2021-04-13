using System;
using System.Collections.Generic;

namespace Marketing.MockContactGenerator.Helpers
{
  public class RandomHelper
  {
    public RandomHelper()
    {
      RandomGen = new System.Random(DateTime.Now.Millisecond);
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

    public String RandomListItem(List<string> list)
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
  }
}