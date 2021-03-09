using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnEXMProject.Models.SitecoreCinema
{
  public class WhatWeKnowAboutYouViewModel
  {
    public KnownData KnownDataXConnect { get; set; }
    public KnownDataTracker KnownDataTracker { get; internal set; }
  }
}