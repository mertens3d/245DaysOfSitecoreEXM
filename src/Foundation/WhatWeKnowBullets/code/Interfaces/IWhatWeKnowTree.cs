using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Interfaces
{
  public interface IWhatWeKnowTree
  {
    ITreeNode Root { get; set; }
    IWhatWeKnowTreeWriter TreeWriter { get; }
  }
}