using LearnEXM.Foundation.CollectionModel.Builder;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.WebApi;
using Sitecore.XConnect.Schema;
using Sitecore.Xdb.Common.Web;
using System;
using System.Collections.Generic;

namespace LearnEXM.Foundation.ConsoleTools.XConnect.Client
{
  public class CFGGenerator
  {
    public XConnectClientConfiguration GetCFG(XdbModel xdbModel)
    {
      var options = CertificateHttpClientHandlerModifierOptions.Parse(CollectionConst.XConnect.Certificate.CertificateStore + CollectionConst.XConnect.Certificate.CertificateThumbprint);

      var certificateModifier = new CertificateHttpClientHandlerModifier(options);

      List<IHttpClientModifier> clientModifiers = new List<IHttpClientModifier>();

      var timeoutClientModifier = new TimeoutHttpClientModifier(new TimeSpan(0, 0, 20));

      clientModifiers.Add(timeoutClientModifier);

      var collectionClient = new CollectionWebApiClient(new Uri(CollectionConst.XConnect.EndPoints.Odata),

        clientModifiers, new[] { certificateModifier });

      var searchClient = new SearchWebApiClient(new Uri(CollectionConst.XConnect.EndPoints.Odata),

        clientModifiers, new[] { certificateModifier }
        );

      var configurationClient = new ConfigurationWebApiClient(new Uri(CollectionConst.XConnect.EndPoints.Configuration),
        clientModifiers, new[] { certificateModifier });

      var cfg = new XConnectClientConfiguration(
        new XdbRuntimeModel(xdbModel),
        collectionClient, searchClient, configurationClient);

      return cfg;
    }
  }
}