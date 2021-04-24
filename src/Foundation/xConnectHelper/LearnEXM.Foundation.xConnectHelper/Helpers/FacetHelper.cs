using Sitecore.Analytics.XConnect.Facets;
using System;

namespace LearnEXM.Foundation.xConnectHelper.Helpers
{

  public class FacetHelper//<T> where T : Facet
  {
    private readonly IXConnectFacets XConnectFacets;

    public FacetHelper(IXConnectFacets xConnectFacets)
    {
      XConnectFacets = xConnectFacets;
    }

    public T SafeGetCreateFacet<T>(string facetKey) where T : Sitecore.XConnect.Facet
    {
      T toReturn;

      if (XConnectFacets?.Facets != null && XConnectFacets.Facets.ContainsKey(facetKey))
      {
        toReturn = (T)Convert.ChangeType(XConnectFacets.Facets[facetKey], typeof(T));
      }
      else
      {
        Sitecore.Diagnostics.Log.Error(Constants.Logger.LoggingPrefix + "facet key not present: " + facetKey, this);
        toReturn = default;
      }

      return toReturn;
    }
  }
}