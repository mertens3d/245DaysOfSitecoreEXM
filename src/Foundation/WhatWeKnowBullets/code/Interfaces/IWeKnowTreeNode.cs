using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Interfaces
{
  public interface IWeKnowTreeNode
  {
    bool HasLeaves { get; }

    string Title { get; set; }
    string Value { get; set; }
    bool ValueIsJson { get; set; }

    void AddNode(IWeKnowTreeNode treeNode);

    void AddNodes(IEnumerable<IWeKnowTreeNode> treeNodes);

    void AddRawNode(string serialized);

    List<IWeKnowTreeNode> GetLeaves();

    string TitleAndValue();
  }
}