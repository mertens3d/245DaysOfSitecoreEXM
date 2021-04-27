using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using Newtonsoft.Json;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Serialization;

namespace LearnEXM.Foundation.WhatWeKnowBullets.TreeNodeFactories
{
  public abstract class _baseFacetTreeNode
  {
    private XConnectClient XConnectClient { get; set; }

    public void SetClient(XConnectClient xConnectClient)
    {
      XConnectClient = xConnectClient;
    }

    public ITreeNode LastModified(Facet facet)
    {
      return new TreeNode("Last Modified", facet.LastModified.ToString());
    }

    public string SerializeFacet(Facet facet)
    {
      var toReturn = string.Empty;

      if (XConnectClient != null)
      {
        var ContractResolver = new XdbJsonContractResolver(XConnectClient.Model, true, true);

        var serializerSettings = new JsonSerializerSettings
        {
          ContractResolver = ContractResolver,
          DateTimeZoneHandling = DateTimeZoneHandling.Utc,
          DefaultValueHandling = DefaultValueHandling.Ignore,
          Formatting = Formatting.Indented,
        };

        var serialized = string.Empty;

        try
        {
          serialized = JsonConvert.SerializeObject(facet, serializerSettings);
          toReturn = serialized;
        }
        catch (System.Exception ex)
        {
          toReturn = "{couldn't serialize}";
        }
      }

      return toReturn;
    }
  }
}