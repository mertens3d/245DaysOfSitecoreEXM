using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories;
using Sitecore.XConnect;
using System.Linq;

namespace LearnEXM.Feature.SitecoreCinema.Models.TreeNodeFactories
{
  internal class CinemaVisitorInfoFacetTreeNodeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = CinemaVisitorInfo.DefaultFacetKey;

    public ITreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new TreeNode("Cinema Visitor Info");
      CinemaVisitorInfo cinemaVisitorInfoFacet = facet as CinemaVisitorInfo;

      if (cinemaVisitorInfoFacet != null)
      {
        toReturn.AddNode(new TreeNode("Favorite Movie", cinemaVisitorInfoFacet.FavoriteMovie));

        var ticketsNode = new TreeNode("Movie Tickets");

        if (cinemaVisitorInfoFacet.MovieTickets != null && cinemaVisitorInfoFacet.MovieTickets.Any())
        {
          ticketsNode.Title = "Movie Tickets (" + cinemaVisitorInfoFacet.MovieTickets.Count + ")";

          foreach (var movieTicket in cinemaVisitorInfoFacet.MovieTickets)
          {
            ticketsNode.AddNode(new TreeNode("Movie Name", movieTicket.MovieName));
          }
        }

        toReturn.AddNode(ticketsNode);
        toReturn.AddNode(LastModified(facet));
        toReturn.AddRawNode(SerializeFacet(facet));
      }
      return toReturn;
    }
  }
}