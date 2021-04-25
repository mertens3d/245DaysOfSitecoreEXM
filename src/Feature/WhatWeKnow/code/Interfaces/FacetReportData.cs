using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using System.Collections.Generic;

namespace LearnEXM.Feature.WhatWeKnow.Interfaces
{
  public class FacetReportData
  {
    public List<IBullet> ChildBullets { get; set; } = new List<IBullet>();
    public string Title { get; set; }
  }
}