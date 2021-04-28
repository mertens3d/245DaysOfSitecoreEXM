using System;
using System.Threading.Tasks;

namespace LearnEXM.Feature.MockContactGenerator
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      bool keepGoing = true;
      var candidateInfoGenerator = new MockContactGenerator();
      var contactCreator = new ContactCreator();

      while (keepGoing)
      {
        CandidateMockContactInfo CandidateContactInfo = candidateInfoGenerator.GetRandomContactInfo();
        Task.Run(async () => { await contactCreator.CreateKnownContact(CandidateContactInfo); }).Wait();

        Console.WriteLine("Press 'Y' to generate another contact");
        var result = Console.ReadKey().Key;
        keepGoing = result == ConsoleKey.Y;
      }
    }
  }
}