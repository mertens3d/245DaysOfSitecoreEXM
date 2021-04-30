using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Interfaces
{
  public interface IWeKnowTreeNode
  {
    bool HasLeaves { get; }
    
    string Title { get; set; }
    string Value { get; set; }
    bool ValueIsJson { get; set; }

    void AddRawNode(string serialized);
    string TitleValue();
    void AddNode(IWeKnowTreeNode treeNode);
    List<IWeKnowTreeNode> GetLeaves();
    void AddNodes(IEnumerable<IWeKnowTreeNode> treeNodes);
  }
}