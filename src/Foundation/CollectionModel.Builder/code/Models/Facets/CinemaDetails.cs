using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.Facets
{
  [Serializable]
  public class CinemaDetails : Facet
  {
    public const string DefaultFacetKey = CollectionConst.FacetKeys.CinemaDetails;
    public string PreferredCinema { get; set; }
  }
}