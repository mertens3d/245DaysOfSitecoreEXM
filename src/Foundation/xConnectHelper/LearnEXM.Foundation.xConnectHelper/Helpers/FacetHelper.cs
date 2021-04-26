using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.xConnectHelper.Helpers
{
  public class FacetHelper
  {
    private readonly IXConnectFacets XConnectFacets;

    public FacetHelper(IXConnectFacets xConnectFacets)
    {
      XConnectFacets = xConnectFacets;
    }


    public Facet GetFacetByKey(string facetKey)
    {
      Facet toReturn = null;

      if(!string.IsNullOrEmpty(facetKey) && XConnectFacets?.Facets != null && XConnectFacets.Facets.ContainsKey(facetKey))
      {
        toReturn = XConnectFacets.Facets[facetKey];
      }
      return toReturn;
    }
    public T SafeGetCreateFacet<T>(string facetKey) where T :  Sitecore.XConnect.Facet
    {
      T toReturn;


      var facet = GetFacetByKey(facetKey);

      if (facet != null)
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