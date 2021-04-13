using Sitecore.Documentation.Helpers;
using Sitecore.Documentation.Models;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using System;
using System.Threading.Tasks;

namespace Sitecore.Documentation
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var contactTestData = new TestData();
      contactTestData.NewContact = new ContactModel()
      {
        ContactIdentifier = "NewContactPrefix_" + Guid.NewGuid().ToString("N")
      };

      MainAsync(args, contactTestData).ConfigureAwait(false).GetAwaiter().GetResult();

      Console.ForegroundColor = ConsoleColor.DarkGreen;
      Console.WriteLine("");
      Console.WriteLine("END OF PROGRAM");
      ReportingHelpers.EnterToProceed();
    }

    private static async Task MainAsync(string[] args, TestData testData)
    {
      var configHelper = new ConfigureHelper();

      var cfg = await configHelper.CreateAndConfigureClient();

      // Initialize a client using the validate configuration

      using (var client = new XConnectClient(cfg))
      {
        try
        {
          var FilterByTime = new Tutorial4();
          await FilterByTime.ExecuteAsync(client);

          var createHelper = new CreateHelper();

          Contact newKnownContact = createHelper.CreateNewContact(testData.NewContact);

          var updateHelper = new UpdateHelper();

          updateHelper.PopulatePersonalInformationFacet(client, newKnownContact);

          client.AddContact(newKnownContact);

          updateHelper.RecordInteractionFacet(client, newKnownContact);

          // Submit contact and interaction - a total of two operations (three)
          await client.SubmitAsync();

          var reportingHelper = new ReportingHelpers();
          reportingHelper.DisplayResults(client);

          var readHelper = new ReadHelper();

          Contact existingContact = await readHelper.RetrieveExistingContactAsync(client);

          if (existingContact != null)
          {
            reportingHelper.DisplayExistingContactData(existingContact);

            var existingContactFacetData = readHelper.RetrieveExistingContactFacetData(client, existingContact);

            reportingHelper.DisplayExistingContactFacetData(existingContactFacetData);

            reportingHelper.ReportInteractionsForExistingContact(existingContact, client);
          }
          else
          {
            Console.WriteLine("ExistingContact is null");
          }

          ReportingHelpers.EnterToProceed();
        }
        catch (XdbExecutionException ex)
        {
          // deal with exception
        }
      }
    }
  }
}