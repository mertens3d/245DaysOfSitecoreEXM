using System;
using System.Collections.Generic;

namespace LearnEXM.Feature.MockContactGenerator
{
  public class MockContactGenerator
  {
    public MockContactGenerator()
    {
      random = new Random();
      var sources = new SourceLists();
      FirstNames = sources.FirstNames;
      LastNames = sources.LastNames;
      Movies = sources.Movies;
    }

    public Random random { get; }
    public List<string> FirstNames { get; private set; }
    public List<string> LastNames { get; private set; }
    public List<string> Movies { get; private set; }

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
          Gender = RandomGender(),
          Id = random.Next(10000)
        }); ;

        idx++;
        if (idx > maxIteration)
        {
          throw new Exception("too many iterations");
        }
      }

      var idxFinal = ContactInfo.Count + 1;
      if (idxFinal > ContactInfo.Count)
      {
        idxFinal = ContactInfo.Count;
      }
      return ContactInfo[random.Next(idxFinal)];
    }

    private string RandomGender()
    {
      return random.Next(2) == 0 ? "Male" : "Female";
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