using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.xConnectHelper.Helpers
{
  public class FacetEditHelper
  {
    private readonly IXConnectFacets XConnectFacets;

    public FacetEditHelper(IXConnectFacets xConnectFacets)
    {
      XConnectFacets = xConnectFacets;
    }

    public Facet GetFacetByKey(string facetKey)
    {
      Facet toReturn = null;

      if (!string.IsNullOrEmpty(facetKey) && XConnectFacets?.Facets != null && XConnectFacets.Facets.ContainsKey(facetKey))
      {
        toReturn = XConnectFacets.Facets[facetKey];
      }
      return toReturn;
    }

    public T SafeGetFacet<T>(string facetKey) where T : Sitecore.XConnect.Facet
    {
      T toReturn;

      var facet = GetFacetByKey(facetKey);

      if (facet != null)
      {
        toReturn = (T)Convert.ChangeType(XConnectFacets.Facets[facetKey], typeof(T));
      }
      else
      {
        Sitecore.Diagnostics.Log.Debug(Constants.Logger.LoggingPrefix + "facet key not present: " + facetKey, this);
        toReturn = default;
      }

      return toReturn;
    }
  }
}