using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using LearnEXM.Foundation.WhatWeKnowBullets.TreeNodeFactories;
using Sitecore.XConnect;

namespace LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models.BulletFactories
{
  internal class CinemaInfoTreeNodeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = CinemaInfo.DefaultFacetKey;

    public ITreeNode BuildTreeNode(Facet facet)
    {
      CinemaInfo cinemaInfo = facet as CinemaInfo;
      var toReturn = new TreeNode("Cinema Info");

      if (cinemaInfo != null)
      {
        toReturn.Leaves.Add(LastModified(facet));
        toReturn.Leaves.Add(SerializeAsRaw(facet));

      }
      return toReturn;
    }
  }
}