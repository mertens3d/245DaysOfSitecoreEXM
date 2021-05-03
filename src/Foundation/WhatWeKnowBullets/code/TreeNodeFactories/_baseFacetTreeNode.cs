using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Newtonsoft.Json;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Serialization;
using Facet = Sitecore.XConnect.Facet;

namespace LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories
{
  public abstract class _baseFacetTreeNode
  {
    private XConnectClient XConnectClient { get; set; }
    protected WeKnowTreeOptions TreeOptions { get; set; }

    public _baseFacetTreeNode(WeKnowTreeOptions treeOptions)
    {
      TreeOptions = treeOptions;
    }

    public void SetClient(XConnectClient xConnectClient)
    {
      XConnectClient = xConnectClient;
    }

    public IWeKnowTreeNode LastModified(Facet facet)
    {
      IWeKnowTreeNode toReturn = null;

      if (TreeOptions.IncludeLastModified)
      {
        toReturn = new WeKnowTreeNode("Last Modified", facet.LastModified.ToString(), TreeOptions);
      }

      return toReturn;
    }

   
  }
}