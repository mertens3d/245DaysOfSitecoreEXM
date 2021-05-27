namespace LearnEXM.Foundation.WhatWeKnowTree.Interfaces
{
  public interface IWeKnowTree
  {
    IWeKnowTreeNode Root { get; set; }
    IWeKnowTreeWriter TreeWriter { get; }
  }
}