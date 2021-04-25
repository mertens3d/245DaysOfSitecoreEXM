using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using System.Collections.Generic;

namespace LearnEXM.Feature.WhatWeKnow.Interfaces
{
  public class FacetReportData
  {
    public List<ITreeNode> ChildBullets { get; set; } = new List<ITreeNode>();
    public string Title { get; set; }
  }
}