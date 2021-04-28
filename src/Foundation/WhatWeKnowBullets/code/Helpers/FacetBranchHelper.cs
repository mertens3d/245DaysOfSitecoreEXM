using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect.Client;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers
{
  public class FacetBranchHelper
  {
    public FacetBranchHelper(List<IFacetNodeFactory> customFacetKeyBulletFactories, XConnectClient xConnectClient, IXConnectFacets xConnectFacets)
    {
      CustomFacetKeyBulletFactories = customFacetKeyBulletFactories;
      XConnectClient = xConnectClient;
      XConnectFacets = xConnectFacets;
    }

    public IXConnectFacets XConnectFacets { get; set; }
    private List<IFacetNodeFactory> CustomFacetKeyBulletFactories { get; }
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

    public IWhatWeKnowTreeNode GetFacetTreeNode(string targetFacetKey)
    {
      IWhatWeKnowTreeNode toReturn = null;

      var treeFactory = GetFacetTreeFactoryByKey(targetFacetKey);
      if (treeFactory != null)
      {
        Sitecore.XConnect.Facet facet = GetFacetByKey(targetFacetKey);
        if (facet != null)
        {
          treeFactory.SetClient(XConnectClient);
          toReturn = treeFactory.BuildTreeNode(facet);
        }
      }

      return toReturn;
    }

    private List<IFacetNodeFactory> BuiltInFacetTreeNodeFactories
    {
      get
      {
        return new List<IFacetNodeFactory>
        {
          new EmailAddressListTreeNodeFactory(),
          new AddressListNodeFactory(),
          new PersonalInformationTreeNodeFactory(),
        };
      }
    }

    public IFacetNodeFactory GetFacetTreeFactoryByKey(string facetKey)
    {
      IFacetNodeFactory toReturn = null;

      toReturn = CustomFacetKeyBulletFactories.FirstOrDefault(x => x.AssociatedDefaultFacetKey.Equals(facetKey));
      if (toReturn == null && CustomFacetKeyBulletFactories != null)
      {
        toReturn = BuiltInFacetTreeNodeFactories.FirstOrDefault(x => x.AssociatedDefaultFacetKey.Equals(facetKey));
      }

      return toReturn;
    }

    public IWhatWeKnowTreeNode FoundFacetKeys()
    {
      var toReturn = new WhatWeKnowTreeNode("Found Facet Keys");

      if (XConnectFacets?.Facets != null)
      {
        foreach (KeyValuePair<string, Sitecore.XConnect.Facet> facetPair in XConnectFacets.Facets)
        {
          toReturn.AddNode(new WhatWeKnowTreeNode(facetPair.Key));
        }
      }

      return toReturn;
    }
  }
}