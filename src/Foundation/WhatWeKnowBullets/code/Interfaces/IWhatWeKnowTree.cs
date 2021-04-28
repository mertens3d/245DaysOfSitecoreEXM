using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Interfaces
{
  public interface IWhatWeKnowTree
  {
    IWhatWeKnowTreeNode Root { get; set; }
    IWhatWeKnowTreeWriter TreeWriter { get; }
  }
}