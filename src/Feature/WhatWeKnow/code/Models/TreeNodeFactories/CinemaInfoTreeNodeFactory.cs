using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories;
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
        toReturn.AddNode(LastModified(facet));
        toReturn.AddRawNode(SerializeFacet(facet));
      }
      return toReturn;
    }
  }
}