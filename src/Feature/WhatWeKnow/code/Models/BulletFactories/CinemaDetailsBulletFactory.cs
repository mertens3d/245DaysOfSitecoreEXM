using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using LearnEXM.Foundation.WhatWeKnowBullets.TreeNodeFactories;
using Sitecore.XConnect;

namespace LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models.BulletFactories
{
  internal class CinemaDetailsBulletFactory : _baseFacetTreeNode, IFacetTreeNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = CinemaDetails.DefaultFacetKey;

    public ITreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new TreeNode("Cinema Details");

      var cinemaDetails = facet as CinemaDetails;

      if (cinemaDetails != null)
      {
        toReturn.Leaves.Add(SerializeAsRaw(facet));
      }

      return toReturn;
    }
  }
}