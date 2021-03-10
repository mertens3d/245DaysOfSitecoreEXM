using LearnEXM.Feature.MockContactGenerator;
using System;
using System.Threading.Tasks;

namespace MockContactGenerator
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      bool keepGoing = true;
      var candidateInfoGenerator = new LearnEXM.Feature.MockContactGenerator.MockContactGenerator();
      var contactCreator = new ContactCreator();

      while (keepGoing)
      {
        CandidateContactInfo CandidateContactInfo = candidateInfoGenerator.GetRandomContactInfo();
        Task.Run(async () => { await contactCreator.CreateKnownContact(CandidateContactInfo); }).Wait();

        Console.WriteLine("Press 'Y' to generate another contact");
        var result = Console.ReadKey().Key;
        keepGoing = result == ConsoleKey.Y;
      }
    }
  }
}