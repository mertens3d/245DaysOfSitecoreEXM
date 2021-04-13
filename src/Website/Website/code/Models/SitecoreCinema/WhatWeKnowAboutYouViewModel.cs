using LearnEXM.Feature.WhatWeKnowAboutYou.Models;

namespace LearnEXMProject.Models.SitecoreCinema
{
  public class WhatWeKnowAboutYouViewModel
  {
    public KnownData KnownDataXConnect { get; set; }
    public KnownDataTracker KnownDataTracker { get; internal set; }
  }
}