using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using LearnEXM.Foundation.WhatWeKnowBullets.TreeNodeFactories;
using Sitecore.XConnect;
using System.Linq;

namespace LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models.BulletFactories
{
  internal class CinemaVisitorInfoBulletFactory : _baseFacetTreeNode, IFacetTreeNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = CinemaVisitorInfo.DefaultFacetKey;

    public ITreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new TreeNode("Cinema Visitor Info");
      CinemaVisitorInfo cinemaVisitorInfoFacet = facet as CinemaVisitorInfo;

      if (cinemaVisitorInfoFacet != null)
      {
        toReturn.Leaves.Add(new TreeNode("Favorite Movie", cinemaVisitorInfoFacet.FavoriteMovie));

        var ticketsBullet = new TreeNode("Movie Tickets");

        if (cinemaVisitorInfoFacet.MovieTickets != null && cinemaVisitorInfoFacet.MovieTickets.Any())
        {
          ticketsBullet.Title = "Movie Tickets (" + cinemaVisitorInfoFacet.MovieTickets.Count + ")";

          foreach (var movieTicket in cinemaVisitorInfoFacet.MovieTickets)
          {
            ticketsBullet.Leaves.Add(new TreeNode("Movie Name", movieTicket.MovieName));
          }
        }
        
        toReturn.Leaves.Add(ticketsBullet);

        toReturn.Leaves.Add(SerializeAsRaw(facet));
      }
      return toReturn;
    }
  }
}