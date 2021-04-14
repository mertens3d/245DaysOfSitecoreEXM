using Sitecore.XConnect;
using System;
using LearnEXM.Foundation.Marketing;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.Facets
{
  [Serializable, FacetKey(DefaultFacetKey)]
  public class Marketing : Facet
  {
    public const string AddressKey = MarketingConst.FacetKeys.Marketing;
    public const string DefaultFacetKey = MarketingConst.FacetKeys.Marketing;
    public string CompanyName { get; set; }
    public string ContactDivision { get; set; }
    public string ContactLineOfBusiness { get; set; }
    public string ContactPhone { get; set; }
    public string ContactType { get; set; }
  }
}