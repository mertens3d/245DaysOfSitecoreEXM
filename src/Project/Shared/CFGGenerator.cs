using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.WebApi;
using Sitecore.XConnect.Schema;
using Sitecore.Xdb.Common.Web;
using System;
using System.Collections.Generic;

namespace Shared
{
  public class Initializer
  {
    public async System.Threading.Tasks.Task InitCFGAsync(XConnectClientConfiguration cfg, string[] successMessage)
    {
      try
      {
        await cfg.InitializeAsync();

        // Print xConnect if configuration is valid
        System.Console.WindowWidth = 160;
        foreach (string line in successMessage)
          System.Console.WriteLine(line);
        System.Console.WriteLine(); // Extra space
      }
      catch (XdbModelConflictException ce)
      {
        System.Console.WriteLine("ERROR:" + ce.Message);
        return;
      }
    }
  }

  public class CFGGenerator
  {
    public XConnectClientConfiguration GetCFG(XdbModel xdbModel)
    {
      CertificateHttpClientHandlerModifierOptions options = CertificateHttpClientHandlerModifierOptions.Parse(Shared.Const.XConnect.Certificate.CertificateStore + Shared.Const.XConnect.Certificate.CertificateThumbprint);

      var certificateModifier = new CertificateHttpClientHandlerModifier(options);

      List<IHttpClientModifier> clientModifiers = new List<IHttpClientModifier>();

      var timeoutClientModifier = new TimeoutHttpClientModifier(new TimeSpan(0, 0, 20));

      clientModifiers.Add(timeoutClientModifier);

      var collectionClient = new CollectionWebApiClient(new Uri(Shared.Const.XConnect.EndPoints.Odata),

        clientModifiers, new[] { certificateModifier });

      var searchClient = new SearchWebApiClient(new Uri(Shared.Const.XConnect.EndPoints.Odata),

        clientModifiers, new[] { certificateModifier }
        );

      var configurationClient = new ConfigurationWebApiClient(new Uri(Shared.Const.XConnect.EndPoints.Configuration),
        clientModifiers, new[] { certificateModifier });

      var cfg = new XConnectClientConfiguration(
        new XdbRuntimeModel(xdbModel),
        collectionClient, searchClient, configurationClient);

      return cfg;
    }
  }
}