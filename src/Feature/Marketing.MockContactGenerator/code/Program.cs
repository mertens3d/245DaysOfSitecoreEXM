using Marketing.MockContactGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Marketing.MockContactGenerator.Helpers.FeedbackHelper;

namespace Marketing.MockContactGenerator
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      bool keepGoing = true;
      var candidateInfoGenerator = new MockContactGeneratorHelper();
      var xConnectBroker = new XConnectBroker();

      bool brokerResult = false;
      Task.Run(async () => { brokerResult = await xConnectBroker.InitBrokerAsync(); }).Wait();

      if (brokerResult)
      {
        var maxIteration = 50;
        var interactionCheck = 0;

        while (keepGoing && interactionCheck < maxIteration)
        {
          CandidateContactInfo candidate = candidateInfoGenerator.GenerateContact();
          Task.Run(async () => { await xConnectBroker.AddKnownContactAsync(candidate); }).Wait();
          Task.Run(async () => { await xConnectBroker.ReportOnKnownContactAsync(candidate); }).Wait();

          interactionCheck++;

          Console.WriteLine("Press 'Y' to generate another contact");
          var response = Console.ReadKey().Key;
          keepGoing = response == ConsoleKey.Y;
        }
      }
      else
      {
        Console.WriteLine("Broker not Init");
      }

      Console.WriteLine("Press any key");
      Console.ReadKey();
    }
  }
}
