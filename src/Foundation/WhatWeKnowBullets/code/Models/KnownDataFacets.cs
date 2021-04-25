using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Models
{
  public class KnownDataFacets
  {
    public List<IBullet> BulletReports { get; set; } = new List<IBullet>();
  }
}