using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories;
using Sitecore.XConnect;

namespace LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models.BulletFactories
{
  internal class CinemaDetailsFacetTreeNodeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = CinemaDetails.DefaultFacetKey;

    public ITreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new TreeNode("Cinema Details");

      var cinemaDetails = facet as CinemaDetails;

      if (cinemaDetails != null)
      {
        toReturn.AddRawNode(SerializeFacet(facet));
        toReturn.AddNode(LastModified(facet));
      }

      return toReturn;
    }
  }
}