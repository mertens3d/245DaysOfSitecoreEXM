using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect.Client;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers.NodeBuilders
{
  public class FacetsNodeBuilder
  {
    public FacetsNodeBuilder(XConnectClient xConnectClient, IXConnectFacets xConnectFacets, WeKnowTreeOptions treeOptions)
    {
      XConnectClient = xConnectClient;
      XConnectFacets = xConnectFacets;
      TreeOptions = treeOptions;
    }

    private IXConnectFacets XConnectFacets { get; set; }
    private WeKnowTreeOptions TreeOptions { get; }
    private XConnectClient XConnectClient { get; }

    public Sitecore.XConnect.Facet GetFacetByKey(string facetKey)
    {
      Sitecore.XConnect.Facet toReturn = null;

      if (!string.IsNullOrEmpty(facetKey) && XConnectFacets?.Facets != null && XConnectFacets.Facets.ContainsKey(facetKey))
      {
        toReturn = XConnectFacets.Facets[facetKey];
      }
      return toReturn;
    }

    public IWeKnowTreeNode BuildFacetsNode(string targetFacetKey)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) GetFacetTreeNode: " + typeof(FacetsNodeBuilder).Name);
      IWeKnowTreeNode toReturn = null;


      Sitecore.XConnect.Facet facet = GetFacetByKey(targetFacetKey);
      IFacetNodeFactory treeFactory = new GenericFacetBranchFactory(targetFacetKey, TreeOptions, XConnectClient);

      if (facet != null)
      {
        treeFactory.SetClient(XConnectClient);
        toReturn = treeFactory.BuildTreeNode(facet);
      }

      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "e) GetFacetTreeNode: " + typeof(FacetsNodeBuilder).Name);
      return toReturn;
    }

    public IWeKnowTreeNode FoundFacetKeys()
    {
      var toReturn = new WeKnowTreeNode("Found Facet Keys", TreeOptions);

      if (XConnectFacets?.Facets != null)
      {
        foreach (KeyValuePair<string, Sitecore.XConnect.Facet> facetPair in XConnectFacets.Facets)
        {
          toReturn.AddNode(new WeKnowTreeNode(facetPair.Key, TreeOptions));
        }
      }

      return toReturn;
    }
  }
}