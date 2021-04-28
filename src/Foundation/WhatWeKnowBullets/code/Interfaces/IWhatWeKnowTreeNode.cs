using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Interfaces
{
  public interface IWhatWeKnowTreeNode
  {
    bool HasLeaves { get; }
    
    string Title { get; set; }
    string Value { get; set; }
    bool ValueIsJson { get; set; }

    void AddRawNode(string serialized);
    string TitleValue();
    void AddNode(IWhatWeKnowTreeNode treeNode);
    List<IWhatWeKnowTreeNode> GetLeaves();
    void AddNodes(IEnumerable<IWhatWeKnowTreeNode> treeNodes);
  }
}