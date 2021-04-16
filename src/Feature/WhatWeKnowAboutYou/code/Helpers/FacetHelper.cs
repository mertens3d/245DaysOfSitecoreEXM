using LearnEXM.Foundation.CollectionModel.Builder;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect.Collection.Model;
using System;

namespace LearnEXM.Feature.WhatWeKnowAboutYou.Helpers
{
  public class FacetHelper//<T> where T : Facet
  {
    private readonly IXConnectFacets XConnectFacets;

    public FacetHelper(IXConnectFacets xConnectFacets)
    {
      this.XConnectFacets = xConnectFacets;
    }

    public T SafeGetFacet<T>(string facetKey)
    {
      T toReturn;

      if (XConnectFacets.Facets.ContainsKey(facetKey))
      {
        toReturn = (T)Convert.ChangeType(XConnectFacets.Facets[facetKey], typeof(T));
      }
      else
      {
        Sitecore.Diagnostics.Log.Error(CollectionConst.Logger.CinemaPrefix + "facet key not present: " + PersonalInformation.DefaultFacetKey, this);
        toReturn = default(T);
      }

      return toReturn;
    }
  }
}