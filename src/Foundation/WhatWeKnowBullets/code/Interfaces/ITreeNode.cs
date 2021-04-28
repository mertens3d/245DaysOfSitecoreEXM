using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Interfaces
{
  public interface ITreeNode
  {
    bool HasLeaves { get; }
    
    string Title { get; set; }
    string Value { get; set; }
    bool ValueIsJson { get; set; }

    void AddRawNode(string serialized);
    string TitleValue();
    void AddNode(ITreeNode treeNode);
    List<ITreeNode> GetLeaves();
    void AddNodes(IEnumerable<ITreeNode> treeNodes);
  }
}