using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnEXMProject.Models.SitecoreCinema
{
  public class WhatWeKnowAboutYouViewModel
  {
    public KnownDataXConnect KnownDataXConnect { get; set; }
    public KnownDataTracker KnownDataTracker { get; internal set; }
  }
}