using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using Sitecore.XConnect;
using System.Linq;

namespace LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models.BulletFactories
{
  internal class CinemaVisitorInfoBulletFactory : IFacetBulletFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = CinemaVisitorInfo.DefaultFacetKey;

    public IBullet GetBullet(Facet facet)
    {
      var toReturn = new Bullet("Cinema Visitor Info");
      CinemaVisitorInfo cinemaVisitorInfoFacet = facet as CinemaVisitorInfo;

      if (cinemaVisitorInfoFacet != null)
      {
        toReturn.ChildBullets.Add(new Bullet("Favorite Movie", cinemaVisitorInfoFacet.FavoriteMovie));

        var ticketsBullet = new Bullet("Movie Tickets");

        if (cinemaVisitorInfoFacet.MovieTickets != null && cinemaVisitorInfoFacet.MovieTickets.Any())
        {
          ticketsBullet.Title = "Movie Tickets (" + cinemaVisitorInfoFacet.MovieTickets.Count + ")";

          foreach (var movieTicket in cinemaVisitorInfoFacet.MovieTickets)
          {
            ticketsBullet.ChildBullets.Add(new Bullet("Movie Name", movieTicket.MovieName));
          }
        }
      }
      return toReturn;
    }
  }
}