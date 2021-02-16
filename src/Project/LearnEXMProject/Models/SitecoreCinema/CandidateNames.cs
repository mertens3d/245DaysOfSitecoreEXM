using System;
using System.Collections.Generic;

namespace LearnEXMProject.Models.SitecoreCinema
{
  public class CandidateNames
  {
    private List<string> firstNames = new List<string>
    {
      "Bob",
      "Susan",
      "Gregory",
      "Pete",
      "Scooby",
      "Marvin" ,
      "Wendy",
          };

    private List<string> LastNames = new List<string>
    {
      "Hope",
      "B. Anthony",
      "Mertens",
      "Navarra",
      "Do",
      "Martian",
      "Red"
    };

    public CandidateNames()
    {
      random = new Random();
    }

    public Random random { get; }
    public List<CandidateNamePair> GetSomeNames()
    {
      firstNames = Shuffle(firstNames);
      LastNames = Shuffle(LastNames);

      List<CandidateNamePair> namePairs = new List<CandidateNamePair>();

      int idx = 0;
      int maxIteration = 100000;
      foreach (var firstName in firstNames)
      {
        namePairs.Add(new CandidateNamePair
        {
          FirstName = firstName,
          LastName = LastNames[idx]
        }); ;

        idx++;
        if (idx > maxIteration)
        {
          throw new Exception("too many iterations");
        }
      }
      return namePairs;
    }

    private List<string> Shuffle(List<string> names)
    {
     

      int n = names.Count;

      while (n > 1)
      {
        n--;
        int k = random.Next(n + 1);
        string hold = names[k];
        names[k] = names[n];
        names[n] = hold;
      }

      return names;
    }
  }
}