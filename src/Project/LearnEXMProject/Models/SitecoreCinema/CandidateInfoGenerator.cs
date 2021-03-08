using System;
using System.Collections.Generic;

namespace LearnEXMProject.Models.SitecoreCinema
{
  public class CandidateInfoGenerator
  {
    private List<string> Movies = new List<string>
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


    private List<string> FirstNames = new List<string>
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

    private List<string> LastNames = new List<string>
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

    public CandidateInfoGenerator()
    {
      random = new Random();
    }

    public Random random { get; }

    public CandidateContactInfo GetRandomContactInfo()
    {
      FirstNames = Shuffle(FirstNames);
      LastNames = Shuffle(LastNames);
      Movies = Shuffle(Movies);

      List<CandidateContactInfo> ContactInfo = new List<CandidateContactInfo>();

      int idx = 0;
      int maxIteration = 100000;
      foreach (var firstName in FirstNames)
      {
        ContactInfo.Add(new CandidateContactInfo
        {
          FirstName = firstName,
          LastName = LastNames[idx],
          FavoriteMovie = Movies[idx],
          Id = random.Next(10000)
        }); ;

        idx++;
        if (idx > maxIteration)
        {
          throw new Exception("too many iterations");
        }
      }

      var idxFinal = ContactInfo.Count + 1;
      if(idxFinal > ContactInfo.Count)
      {
        idxFinal = ContactInfo.Count;
      }
      return ContactInfo[random.Next(idxFinal)];
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