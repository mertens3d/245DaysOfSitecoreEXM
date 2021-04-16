using LearnEXM.Feature.WhatWeKnowAboutYou.Models;

namespace LearnEXM.Project.SitecoreCinema.Model
{
  public class WhatWeKnowAboutYouViewModel
  {
    public KnownData KnownDataXConnect { get; set; }
    public KnownDataTracker KnownDataTracker { get; internal set; }
  }
}