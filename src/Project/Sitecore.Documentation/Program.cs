using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.WebApi;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.Schema;
using Sitecore.Xdb.Common.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sitecore.Documentation
{
  internal class Program
  {
    private static void DisplayXConnectResult()
    {
      // Print xConnect if configuration is valid
      var arr = new[]
       {
                        @"            ______                                                       __     ",
                        @"           /      \                                                     |  \    ",
                        @" __    __ |  $$$$$$\  ______   _______   _______    ______    _______  _| $$_   ",
                        @"|  \  /  \| $$   \$$ /      \ |       \ |       \  /      \  /       \|   $$ \  ",
                        @"\$$\/  $$| $$      |  $$$$$$\| $$$$$$$\| $$$$$$$\|  $$$$$$\|  $$$$$$$ \$$$$$$   ",
                        @" >$$  $$ | $$   __ | $$  | $$| $$  | $$| $$  | $$| $$    $$| $$        | $$ __  ",
                        @" /  $$$$\ | $$__/  \| $$__/ $$| $$  | $$| $$  | $$| $$$$$$$$| $$_____   | $$|  \",
                        @"|  $$ \$$\ \$$    $$ \$$    $$| $$  | $$| $$  | $$ \$$     \ \$$     \   \$$  $$",
                        @" \$$   \$$  \$$$$$$   \$$$$$$  \$$   \$$ \$$   \$$  \$$$$$$$  \$$$$$$$    \$$$$ "
                    };

      Console.WindowWidth = 160;

      foreach (string line in arr)
      {
        Console.WriteLine(line);
      }
    }

    private static async Task<XConnectClientConfiguration> CreateAndConfigureClient()
    {
      XConnectClientConfiguration cfg = null;

      try
      {
        CertificateHttpClientHandlerModifierOptions options = CertificateHttpClientHandlerModifierOptions.Parse(Const.XConnect.Certificate.CertificateStore + Const.XConnect.Certificate.CertificateThumbprint);

        var certificateModifier = new CertificateHttpClientHandlerModifier(options);

        List<IHttpClientModifier> clientModifiers = new List<IHttpClientModifier>();

        var timeoutClientModifier = new TimeoutHttpClientModifier(new TimeSpan(0, 0, 20));
        clientModifiers.Add(timeoutClientModifier);

        var collectionClient = new CollectionWebApiClient(new Uri(Const.XConnect.EndPoints.Odata), clientModifiers, new[]
        {
          certificateModifier
        });

        var searchClient = new SearchWebApiClient(new Uri(Const.XConnect.EndPoints.Odata), clientModifiers, new[] {
          certificateModifier
        });

        var configurationClient = new ConfigurationWebApiClient(new Uri(Const.XConnect.EndPoints.Configuration), clientModifiers, new[] { certificateModifier });

        cfg = new XConnectClientConfiguration(new XdbRuntimeModel(CollectionModel.Model), collectionClient, searchClient, configurationClient);

        await cfg.InitializeAsync();

        DisplayXConnectResult();
      }
      catch (XdbModelConflictException ce)
      {
        Console.WriteLine("Error: " + ce.Message);
      }

      return cfg;
    }

    private static Contact CreateNewContact(ContactModel newContact)
    {
      // Identifier for a 'known' contact
      var identifier = new ContactIdentifier[]
      {
            new ContactIdentifier(Const.XConnect.ContactIdentifiers.Sources.Twitter , newContact.ContactIdentifier, ContactIdentifierType.Known)
      };

      // Print out identifier that is going to be used
      Console.WriteLine("Identifier: " + identifier[0].Identifier);

      // Create a new contact with the identifier
      Contact knownContact = new Contact(identifier);

      return knownContact;
    }

    private static void DisplayResults(XConnectClient client)
    {
      var operations = client.LastBatch;

      Console.WriteLine("RESULTS...");

      // Loop through operations and check status
      foreach (var operation in operations)
      {
        Console.WriteLine(operation.OperationType + operation.Target.GetType().ToString() + " Operation: " + operation.Status);
      }
    }

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
      Console.ReadKey();
    }

    private static async Task MainAsync(string[] args, TestData testData)
    {
      var cfg = await CreateAndConfigureClient();

      // Initialize a client using the validate configuration

      using (var client = new XConnectClient(cfg))
      {
        try
        {
          Contact newKnownContact = CreateNewContact(testData.NewContact);
          PopulatePersonalInformationFacet(client, newKnownContact);

          client.AddContact(newKnownContact);

          RecordInteractionFacet(client, newKnownContact);

          // Submit contact and interaction - a total of two operations (three)
          await client.SubmitAsync();

          DisplayResults(client);

          Contact existingContact = await RetrieveExistingContactAsync(client);
          if (existingContact != null)
          {
            DisplayExistingContactData(existingContact);

            var existingContactFacetData = RetrieveExistingContactFacetData(client, existingContact);

            DisplayExistingContactFacetData(existingContactFacetData);
          }
          else
          {
            Console.WriteLine("ExistingContact is null");
          }

          Console.ReadLine();
        }
        catch (XdbExecutionException ex)
        {
          // deal with exception
        }
      }
    }

    private static void DisplayExistingContactData(Contact existingContact)
    {
        Console.WriteLine("Contact ID: " + existingContact.Id.ToString());
    }

    private static void DisplayExistingContactFacetData(PersonalInformation existingContact)
    {
      if (existingContact != null)
      {
        Console.WriteLine("Contact Name: " + existingContact.FirstName);
      }
      else
      {
        Console.WriteLine("ExistingContactFaceData is null");
      }
    }

    private static void PopulatePersonalInformationFacet(XConnectClient client, Contact knownContact)
    {
      PersonalInformation personalInformationFacet = new PersonalInformation();

      personalInformationFacet.FirstName = "Myrtle";
      personalInformationFacet.LastName = "McSitecore";
      personalInformationFacet.JobTitle = "Programmer Writer";

      client.SetFacet<PersonalInformation>(knownContact, PersonalInformation.DefaultFacetKey, personalInformationFacet);
    }

    private static void RecordInteractionFacet(XConnectClient client, Contact knownContact)
    {
      var channelId = Const.XConnect.ChannelIds.OtherEvent;
      var offlineGoal = Const.XConnect.Goals.WatchedDemo;

      // Create a new interaction for that contact
      Interaction interaction = new Interaction(knownContact, InteractionInitiator.Brand, channelId, "");

      // add events - all interactions must have at least one event
      var xConnectEvent = new Goal(offlineGoal, DateTime.UtcNow);
      interaction.Events.Add(xConnectEvent);

      IpInfo ipInfo = new IpInfo("127.0.0.1");
      ipInfo.BusinessName = "Home";

      client.SetFacet<IpInfo>(interaction, IpInfo.DefaultFacetKey, ipInfo);

      // Add the contact and interaction
      client.AddInteraction(interaction);
    }

    private static async Task<Contact> RetrieveExistingContactAsync(XConnectClient client)
    {
      Contact existingContact = null;

      try
      {
        IdentifiedContactReference reference = new IdentifiedContactReference(Const.XConnect.ContactIdentifiers.Sources.Twitter, Const.XConnect.ContactIdentifiers.ExampleData.MyrtleIdentifier);
        existingContact = await client.GetAsync<Contact>(reference, new ContactExpandOptions(new string[] { PersonalInformation.DefaultFacetKey }));
      }
      catch (Exception ex)
      {
        throw;
      }

      return existingContact;
    }

    private static PersonalInformation RetrieveExistingContactFacetData(XConnectClient client, Contact existingContact)
    {
      PersonalInformation existingContactFacet = null;

      try
      {
        if (existingContact != null)
        {
          existingContactFacet = existingContact.GetFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey);
        }
        else
        {
          Console.WriteLine($"Identifier {Const.XConnect.ContactIdentifiers.ExampleData.MyrtleIdentifier} not found");
        }
      }
      catch (Exception)
      {
        // Deal with exception
      }

      return existingContactFacet;
    }
  }
}