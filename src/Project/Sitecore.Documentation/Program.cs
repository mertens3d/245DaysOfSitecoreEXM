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
      CertificateHttpClientHandlerModifierOptions options = CertificateHttpClientHandlerModifierOptions.Parse(Const.Certificate.CertificateStore + Const.Certificate.CertificateThumbprint);

      var certificateModifier = new CertificateHttpClientHandlerModifier(options);

      List<IHttpClientModifier> clientModifiers = new List<IHttpClientModifier>();

      var timeoutClientModifier = new TimeoutHttpClientModifier(new TimeSpan(0, 0, 20));
      clientModifiers.Add(timeoutClientModifier);

      var collectionClient = new CollectionWebApiClient(new Uri(Const.EndPoints.Odata), clientModifiers, new[]
      {
        certificateModifier
      });

      var searchClient = new SearchWebApiClient(new Uri(Const.EndPoints.Odata), clientModifiers, new[] {
        certificateModifier
      });

      var configurationClient = new ConfigurationWebApiClient(new Uri(Const.EndPoints.Configuration), clientModifiers, new[] { certificateModifier });

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
        }
        catch (XdbExecutionException ex)
        {
          // deal with exception
        }
      }
    }
  }
}