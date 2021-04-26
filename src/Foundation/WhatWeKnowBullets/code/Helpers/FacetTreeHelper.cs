using LearnEXM.Foundation.WhatWeKnowBullets.BuiltInBulletFactories;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using LearnEXM.Foundation.xConnectHelper.Helpers;
using Sitecore.XConnect.Client;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Helpers
{
  public class FacetTreeHelper
  {
    public FacetTreeHelper(List<IFacetNodeFactory> customFacetKeyBulletFactories, XConnectClient xConnectClient)
    {
      CustomFacetKeyBulletFactories = customFacetKeyBulletFactories;
      XConnectClient = xConnectClient;
    }

    private List<IFacetNodeFactory> CustomFacetKeyBulletFactories { get; }
    private XConnectClient XConnectClient { get; }

    public ITreeNode GetFacetTreeNode(string targetFacetKey, FacetHelper facetHelper)
    {
      ITreeNode toReturn = null;

      var treeFactory = GetFacetTreeFactoryByKey(targetFacetKey);
      if (treeFactory != null)
      {
        var facet = facetHelper.GetFacetByKey(targetFacetKey);
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
          new PersonalInformationTreeNodeFactory()
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
  }
}