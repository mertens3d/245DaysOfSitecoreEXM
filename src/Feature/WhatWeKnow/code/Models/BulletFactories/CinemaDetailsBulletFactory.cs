using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using Sitecore.XConnect;

namespace LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models.BulletFactories
{
  internal class CinemaDetailsBulletFactory : IFacetBulletFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = CinemaDetails.DefaultFacetKey;

    public IBullet GetBullet(Facet facet)
    {
      var toReturn = new Bullet("Cinema Details");

      var cinemaDetails = facet as CinemaDetails;


      if(cinemaDetails != null)
      {

      }

      return toReturn;
    }
  }
}