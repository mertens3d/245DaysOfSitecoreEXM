using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Interfaces
{
  public interface IWhatWeKnowTree
  {
    ITreeNode Root { get; set; }
    IWhatWeKnowTreeWriter TreeWriter { get; }
  }
}