using Sitecore.XConnect;
using System;

namespace Shared.Models.SitecoreCinema
{
  [Serializable, FacetKey(DefaultFacetKey)]
  public class CinemaVisitorInfo : Facet
  {
    public const string DefaultFacetKey = Const.FacetKeys.CinemaVisitorInfo;
    public string FavoriteMovie { get; set; } // Plain text; e.g. "some movie name"

    public CinemaVisitorInfo()
    {

    }
  }
}