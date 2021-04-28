using LearnEXM.Foundation.Marketing;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.WebApi;
using Sitecore.XConnect.Schema;
using Sitecore.Xdb.Common.Web;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace LearnEXM.Feature.MockContactGenerator.Helpers
{
  internal class CFGHelper
  {
    internal static XConnectClientConfiguration GenerateCFG(XdbModel xdbModel)
    {
      XConnectClientConfiguration toReturn = null;

      var CertificateThumbprint = ConfigurationManager.AppSettings.Get("CertificateThumbprint");

      if (!string.IsNullOrEmpty(CertificateThumbprint))
      {
        var options = CertificateHttpClientHandlerModifierOptions.Parse(Const.XConnect.CertificateStorePrefix + CertificateThumbprint);

        var certificateModifier = new CertificateHttpClientHandlerModifier(options);

        List<IHttpClientModifier> clientModifiers = new List<IHttpClientModifier>();

        var timeoutClientModifier = new TimeoutHttpClientModifier(new TimeSpan(0, 0, 20));
        clientModifiers.Add(timeoutClientModifier);

        var odataUri = new Uri(MarketingConst.XConnect.EndPoints.Odata);
        var ConfigurationUri = new Uri(MarketingConst.XConnect.EndPoints.Configuration);
        var httpClientHandlerModifiers = new[] { certificateModifier };

        var collectionClient = new CollectionWebApiClient(odataUri, clientModifiers, httpClientHandlerModifiers);
        var searchClient = new SearchWebApiClient(odataUri, clientModifiers, httpClientHandlerModifiers);
        var configurationClient = new ConfigurationWebApiClient(ConfigurationUri, clientModifiers, httpClientHandlerModifiers);

        toReturn = new XConnectClientConfiguration(new XdbRuntimeModel(xdbModel), collectionClient, searchClient, configurationClient);
      }
      else
      {
        Console.WriteLine("Missing certificate thumbprint. Re: example config");
      }

      return toReturn;
    }
  }
}