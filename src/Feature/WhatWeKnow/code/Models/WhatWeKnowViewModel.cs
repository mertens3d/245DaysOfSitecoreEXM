using LearnEXM.Feature.WhatWeKnow.Interfaces;
using System.Collections.Generic;

namespace LearnEXM.Feature.WhatWeKnow.Models
{
  public class WhatWeKnowViewModel
  {
    public KnownData KnownDataXConnect { get; set; }
    public KnownDataTracker KnownDataTracker { get; internal set; }
    public List<FacetReportData> CustomFacetReport { get; set; } = new List<FacetReportData>();
  }
}