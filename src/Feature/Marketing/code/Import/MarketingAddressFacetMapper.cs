using LearnEXM.Foundation.Marketing;
using Sitecore.ListManagement.Import;
using Sitecore.ListManagement.XConnect.Web.Import;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Feature.Marketing.Import
{
  public class MarketingAddressFacetMapper : IFacetMapper
  {
    private const string FacetMapperPrefix = "MarketingAddress_";

    private readonly PreferredAddressFacetMapper mapper;

    public MarketingAddressFacetMapper() : this(new PreferredAddressFacetMapper())
    {
    }

    public MarketingAddressFacetMapper(PreferredAddressFacetMapper mapper)
    {
      this.mapper = mapper;
    }

    public MappingResult Map(
      string facetKey,
      Facet source,
      ContactMappingInfo mappings,
      string[] data)
    {
      var result = mapper.Map(facetKey, source, mappings, data);
      var facetMappedResult = result as FacetMapped;
      if (facetMappedResult == null)
      {
        return result;
      }
      var partiallyMappedFacet = facetMappedResult.Facet as AddressList;
      if (partiallyMappedFacet == null)
      {
        return result;
      }

      var marketingAddress = new Address();
      var addressLine1 = mappings.GetValue(FormatDataField(nameof(marketingAddress.AddressLine1)), data);
      var addressLine2 = mappings.GetValue(FormatDataField(nameof(marketingAddress.AddressLine2)), data);
      var addressLine3 = mappings.GetValue(FormatDataField(nameof(marketingAddress.AddressLine3)), data);
      var addressLine4 = mappings.GetValue(FormatDataField(nameof(marketingAddress.AddressLine4)), data);
      var city = mappings.GetValue(FormatDataField(nameof(marketingAddress.City)), data);
      var country = mappings.GetValue(FormatDataField(nameof(marketingAddress.CountryCode)), data);
      var postalCode = mappings.GetValue(FormatDataField(nameof(marketingAddress.PostalCode)), data);
      var stateProvince = mappings.GetValue(FormatDataField(nameof(marketingAddress.StateOrProvince)), data);
      if (!string.IsNullOrWhiteSpace(addressLine1)) { marketingAddress.AddressLine1 = addressLine1; }
      if (!string.IsNullOrWhiteSpace(addressLine2)) { marketingAddress.AddressLine2 = addressLine2; }
      if (!string.IsNullOrWhiteSpace(addressLine3)) { marketingAddress.AddressLine3 = addressLine3; }
      if (!string.IsNullOrWhiteSpace(addressLine4)) { marketingAddress.AddressLine4 = addressLine4; }
      if (!string.IsNullOrWhiteSpace(city)) { marketingAddress.City = city; }
      if (!string.IsNullOrWhiteSpace(country)) { marketingAddress.CountryCode = country; }
      if (!string.IsNullOrWhiteSpace(postalCode)) { marketingAddress.PostalCode = postalCode; }
      if (!string.IsNullOrWhiteSpace(stateProvince)) { marketingAddress.StateOrProvince = stateProvince; }

      if (partiallyMappedFacet.Others.ContainsKey(MarketingConst.FacetKeys.Marketing))
      {
        partiallyMappedFacet.Others[MarketingConst.FacetKeys.Marketing] = marketingAddress;
      }
      else
      {
        partiallyMappedFacet.Others.Add(MarketingConst.FacetKeys.Marketing, marketingAddress);
      }

      return result;
    }

    private string FormatDataField(string suffix)
    {
      return $"{FacetMapperPrefix}{suffix}";
    }
  }
}