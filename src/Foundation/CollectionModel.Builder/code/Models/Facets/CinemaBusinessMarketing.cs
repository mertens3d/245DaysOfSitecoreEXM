using Sitecore.XConnect;
using System;
using LearnEXM.Foundation.Marketing;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.Facets
{
  [Serializable, FacetKey(DefaultFacetKey)]
  public class CinemaBusinessMarketing : Facet
  {
    public const string AddressKey = MarketingConst.FacetKeys.CinemaBusinessMarketing;
    public const string DefaultFacetKey = MarketingConst.FacetKeys.CinemaBusinessMarketing;
    public string CompanyName { get; set; }
    public string ContactDivision { get; set; }
    public string ContactLineOfBusiness { get; set; }
    public string ContactPhone { get; set; }
    public string ContactType { get; set; }
  }
}