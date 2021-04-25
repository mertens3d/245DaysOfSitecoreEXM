using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Interfaces
{
  public interface ITreeNode
  {
    bool HasLeaves { get; }
    List<ITreeNode> Leaves { get; set; }
    string Title { get; set; }
    string Value { get; set; }
    string TitleValue();
  }
}