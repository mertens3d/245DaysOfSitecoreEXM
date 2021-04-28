using LearnEXM.Feature.MockContactGenerator.Helpers;
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

    public CandidateMockContactInfo GetRandomContactInfo()
    {
      var toReturn = new MockContactGeneratorHelper().GenerateContact();

      return toReturn;
    }

  }
}