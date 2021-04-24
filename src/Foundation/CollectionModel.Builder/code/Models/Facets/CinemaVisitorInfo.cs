using Sitecore.XConnect;
using System;
using System.Collections.Generic;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.Facets
{

  [Serializable, FacetKey(DefaultFacetKey)]
  public class CinemaVisitorInfo : Facet
  {
    public const string DefaultFacetKey = CollectionConst.FacetKeys.CinemaVisitorInfo;
    public string FavoriteMovie { get; set; } // Plain text; e.g. "some movie name"

    public List<MovieTicket> MovieTickets { get; set; } = new List<MovieTicket>();
    public List<Guid> OwnedMovieTickets { get; set; } = new List<Guid>();

    public CinemaVisitorInfo()
    {
    }
  }
}