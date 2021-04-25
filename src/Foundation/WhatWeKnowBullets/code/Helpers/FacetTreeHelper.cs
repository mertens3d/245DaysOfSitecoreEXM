using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using LearnEXM.Foundation.xConnectHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Helpers
{
 public class FacetTreeHelper
  {

    public FacetTreeHelper(List<IFacetTreeNodeFactory> customFacetKeyBulletFactories)
    {
      CustomFacetKeyBulletFactories = customFacetKeyBulletFactories;
    }

    private List<IFacetTreeNodeFactory> CustomFacetKeyBulletFactories { get; }
    public ITreeNode GetFacetTreeNode(string targetFacetKey, FacetHelper facetHelper)
    {
      ITreeNode toReturn = null;

      var matchingFactory = GetMatchingFactory(targetFacetKey);
      if (matchingFactory != null)
      {
        var facet = facetHelper.GetFacetByKey(targetFacetKey);
        if (facet != null)
        {
          toReturn = matchingFactory.BuildTreeNode(facet);
        }
      }

      return toReturn;
    }
    private List<IFacetTreeNodeFactory> BuiltInFacetTreeNodeFactories
    {
      get
      {
        return new List<IFacetTreeNodeFactory>
        {
          new EmailAddressListTreeNodeFactory(),
          new PersonalInformationTreeNodeFactory()
        };
      }
    }
    public IFacetTreeNodeFactory GetMatchingFactory(string facetKey)
    {
      IFacetTreeNodeFactory toReturn = null;

      toReturn = BuiltInFacetTreeNodeFactories.FirstOrDefault(x => x.AssociatedDefaultFacetKey.Equals(facetKey));
      if (toReturn == null && CustomFacetKeyBulletFactories != null)
      {
        toReturn = CustomFacetKeyBulletFactories.FirstOrDefault(x => x.AssociatedDefaultFacetKey.Equals(facetKey));
      }

      return toReturn;
    }

  }
}
