using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.WebApi;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.Schema;
using Sitecore.Xdb.Common.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sitecore.Documentation.Helpers
{
  public class ConfigureHelper
  {
    public async Task<XConnectClientConfiguration> CreateAndConfigureClient()
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
  }
}