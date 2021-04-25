using LearnEXM.Feature.WhatWeKnow.Models;
using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;

namespace LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models
{
  public class WhatWeKnowViewModel
  {
    public KnownData KnownDataXConnect { get; set; }
    public KnownDataTracker KnownDataTracker { get; internal set; }
  }
}