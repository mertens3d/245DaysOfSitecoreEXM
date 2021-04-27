using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using LearnEXM.Foundation.WhatWeKnowBullets.TreeNodeFactories;
using Sitecore.XConnect;
using System;

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
        toReturn.AddRawLeaf(SerializeFacet(facet));
        toReturn.Leaves.Add(LastModified(facet));
      }

      return toReturn;
    }

    
  }
}