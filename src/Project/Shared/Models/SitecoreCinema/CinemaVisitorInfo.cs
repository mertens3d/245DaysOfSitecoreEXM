﻿using Sitecore.XConnect;
using System;
using System.Collections.Generic;

namespace Shared.Models.SitecoreCinema
{
  [Serializable, FacetKey(DefaultFacetKey)]
  public class CinemaVisitorInfo : Facet
  {
    public const string DefaultFacetKey = Const.FacetKeys.CinemaVisitorInfo;
    public string FavoriteMovie { get; set; } // Plain text; e.g. "some movie name"

    public List<Guid> OwnedMovieTickets { get; set; } = new List<Guid>();

    public CinemaVisitorInfo()
    {
    }
  }
}