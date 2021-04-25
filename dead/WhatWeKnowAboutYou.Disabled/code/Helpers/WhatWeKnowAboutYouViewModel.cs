using LearnEXM.Feature.WhatWeKnowAboutYou.Interfaces;
using LearnEXM.Feature.WhatWeKnowAboutYou.Models;
using System.Collections.Generic;

namespace LearnEXM.Feature.WhatWeKnowAboutYou.Helpers
{
  public class WhatWeKnowAboutYouViewModel
  {
    public KnownData KnownDataXConnect { get; set; }
    public KnownDataTracker KnownDataTracker { get; internal set; }
    public List<FacetReportData> CustomFacetReport { get; set; } = new List<FacetReportData>();
  }
}