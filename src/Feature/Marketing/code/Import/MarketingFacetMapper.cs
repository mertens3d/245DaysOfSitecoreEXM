using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.Marketing;
using Sitecore.Diagnostics;
using Sitecore.ListManagement.Import;
using Sitecore.ListManagement.XConnect.Web.Import;
using Sitecore.XConnect;

namespace LearnEXM.Feature.Marketing.Import
{
  public class MarketingFacetMapper : IFacetMapper
  {
    private const string FacetMapperPrefix = "Marketing_";

    public MarketingFacetMapper() : this(MarketingConst.FacetKeys.CinemaBusinessMarketing)
    {
    }

    public MarketingFacetMapper(string facetName)
    {
      Assert.ArgumentNotNull(facetName, nameof(facetName));
      FacetName = facetName;
    }

    public string FacetName { get; }

    public MappingResult Map(
      string facetKey,
      Facet source,
      ContactMappingInfo mappings,
  string[] data)
    {
      if (facetKey != FacetName)
      {
        return new NoMatch(facetKey);
      }

      if (facetKey != FacetName)
        return new NoMatch(facetKey);

      if (!(source is CinemaVisitorInfo newMarketingFacet))
        newMarketingFacet = new CinemaVisitorInfo();

      var marketingInfo = newMarketingFacet;
      //var companyName = mappings.GetValue(FormatDataField(nameof(marketingInfo.CompanyName)), data);
      //var division = mappings.GetValue(FormatDataField(nameof(marketingInfo.ContactDivision)), data);
      //var lineOfBusiness = mappings.GetValue(FormatDataField(nameof(marketingInfo.ContactLineOfBusiness)), data);
      //var phone = mappings.GetValue(FormatDataField(nameof(marketingInfo.ContactPhone)), data);
      //var marketingType = mappings.GetValue(FormatDataField(nameof(marketingInfo.ContactType)), data);

      //if (!string.IsNullOrWhiteSpace(companyName)) { marketingInfo.CompanyName = companyName; }
      //if (!string.IsNullOrWhiteSpace(division)) { marketingInfo.ContactDivision = division; }
      //if (!string.IsNullOrWhiteSpace(lineOfBusiness)) { marketingInfo.ContactLineOfBusiness = lineOfBusiness; }
      //if (!string.IsNullOrWhiteSpace(phone)) { marketingInfo.ContactPhone = phone; }
      //if (!string.IsNullOrWhiteSpace(marketingType)) { marketingInfo.ContactType = marketingType; }

      return new FacetMapped(facetKey, marketingInfo);
    }

    private string FormatDataField(string suffix)
    {
      return $"{FacetMapperPrefix}{suffix}";
    }
  }
}