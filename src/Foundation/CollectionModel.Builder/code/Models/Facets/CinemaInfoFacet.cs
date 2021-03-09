using Sitecore.XConnect;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.Facets
{
  [FacetKey(DefaultFacetKey)]
  public class CinemaInfoFacet : Facet
  {
    public const string DefaultFacetKey = CollectionConst.FacetKeys.CinemaInfo;
    public int CinimaId { get; set; } // e.g. SC123567 - all cinemas have a unique identifier
  }
}