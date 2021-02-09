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
    private static void Main(string[] args)
    {
      MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
      Console.ForegroundColor = ConsoleColor.DarkGreen;
      Console.WriteLine("");
      Console.WriteLine("END OF PROGRAM");
      Console.ReadKey();
    }

    private static async Task MainAsync(string[] args)
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

      var cfg = new XConnectClientConfiguration(new XdbRuntimeModel(CollectionModel.Model), collectionClient, searchClient, configurationClient);

      try
      {
        await cfg.InitializeAsync();

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
      catch (XdbModelConflictException ce)
      {
        Console.WriteLine("Error: " + ce.Message);
      }

      // Initialize a client using the validate configuration

      using (var client = new XConnectClient(cfg))
      {
        try
        {
          var offlineGoal = Const.XConnect.Goals.WatchedDemo;
          var channelId = Const.XConnect.ChannelIds.OtherEvent;

          // Identifier for a 'known' contact
          var identifier = new ContactIdentifier[]
          {
            new ContactIdentifier(Const.XConnect.ContactIdentifiers.Sources.Twitter , "myrtlesitecore" + Guid.NewGuid().ToString("N"), ContactIdentifierType.Known)
          };

          // Print out identifier that is going to be used
          Console.WriteLine("Identifier: " + identifier[0].Identifier);

          // Create a new contact with the identifier
          Contact knownContact = new Contact(identifier);

          client.AddContact(knownContact);


          // Create a new interaction for that contact
          Interaction interaction = new Interaction(knownContact, InteractionInitiator.Brand, channelId, "");


          // add events - all interactions must have at least one event
          var xConnectEvent = new Goal(offlineGoal, DateTime.UtcNow);
          interaction.Events.Add(xConnectEvent);

          // Add the contact and interaction
          client.AddInteraction(interaction);

          // Submit contact and interaction - a total of two operations
          await client.SubmitAsync();

          var operations = client.LastBatch;

          Console.WriteLine("RESULTS...");

          // Loop through operations and check status
          foreach (var operation in operations)
          {
            Console.WriteLine(operation.OperationType + operation.Target.GetType().ToString() + " Operation: " + operation.Status);
          }

          Console.ReadLine();
        }
        catch (XdbExecutionException ex)
        {
          // deal with exception
        }
      }
    }
  }
}