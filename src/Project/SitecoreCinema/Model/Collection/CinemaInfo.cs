﻿using Sitecore.XConnect;

namespace SitecoreCinema.Model.Collection
{
  [FacetKey(DefaultFacetKey)]
  public class CinemaInfo : Facet
  {
    public const string DefaultFacetKey = Const.FacetKeys.CinemaInfo;
    public int CinimaId { get; set; } // e.g. SC123567 - all cinemas have a unique identifier
  }
}