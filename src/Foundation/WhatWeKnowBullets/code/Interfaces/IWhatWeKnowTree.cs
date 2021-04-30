using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Interfaces
{
  public interface IWhatWeKnowTree
  {
    IWeKnowTreeNode Root { get; set; }
    IWhatWeKnowTreeWriter TreeWriter { get; }
  }
}