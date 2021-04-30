using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect.Client;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers
{
  public class FacetBranchHelper
  {
    public FacetBranchHelper( XConnectClient xConnectClient, IXConnectFacets xConnectFacets)
    {
      XConnectClient = xConnectClient;
      XConnectFacets = xConnectFacets;
    }

    public IXConnectFacets XConnectFacets { get; set; }
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

    public IWeKnowTreeNode GetFacetTreeNode(string targetFacetKey)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) GetFacetTreeNode: " + typeof(FacetBranchHelper).Name);
      IWeKnowTreeNode toReturn = null;

      IFacetNodeFactory treeFactory = null; // GetFacetTreeFactoryByKey(targetFacetKey);
      if (treeFactory != null)
      {
        Sitecore.XConnect.Facet facet = GetFacetByKey(targetFacetKey);
        if (facet != null)
        {
          treeFactory.SetClient(XConnectClient);
          toReturn = treeFactory.BuildTreeNode(facet);
        }
      }
      else
      {
        Sitecore.XConnect.Facet facet = GetFacetByKey(targetFacetKey);
        treeFactory = new GenericTreeFactory(targetFacetKey);

        if (facet != null)
        {
          treeFactory.SetClient(XConnectClient);
          toReturn = treeFactory.BuildTreeNode(facet);
        }

        Sitecore.Diagnostics.Log.Error(ProjConstants.Logger.Prefix + "treeFactory is null", this);
      }

      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "e) GetFacetTreeNode: " + typeof(FacetBranchHelper).Name);
      return toReturn;
    }

    public IWeKnowTreeNode FoundFacetKeys()
    {
      var toReturn = new WeKnowTreeNode("Found Facet Keys");

      if (XConnectFacets?.Facets != null)
      {
        foreach (KeyValuePair<string, Sitecore.XConnect.Facet> facetPair in XConnectFacets.Facets)
        {
          toReturn.AddNode(new WeKnowTreeNode(facetPair.Key));
        }
      }

      return toReturn;
    }
  }
}