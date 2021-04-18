using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.Facets
{
  [Serializable]
  [FacetKey(DefaultFacetKey)]
  public class CinemaInfo : Facet
  {
    public const string DefaultFacetKey = CollectionConst.FacetKeys.CinemaInfo;
    public int CinimaId { get; set; } // e.g. SC123567 - all cinemas have a unique identifier
  }
}