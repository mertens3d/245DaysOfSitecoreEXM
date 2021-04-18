using LearnEXM.Foundation.CollectionModel.Builder;
using Sitecore.Analytics.Model.Framework;
using Sitecore.Analytics.XConnect.Facets;
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

    public T SafeGetCreateFacet<T>(string facetKey) where T : Sitecore.XConnect.Facet
    {
      T toReturn;

      if (XConnectFacets.Facets.ContainsKey(facetKey))
      {
        toReturn = (T)Convert.ChangeType(XConnectFacets.Facets[facetKey], typeof(T));
      }
      //else
      //{
      //  toReturn = new T();
      //}

      else
      {
        Sitecore.Diagnostics.Log.Error(CollectionConst.Logger.CinemaPrefix + "facet key not present: " + facetKey, this);
        toReturn = default(T);
      }

      return toReturn;
    }
  }
}