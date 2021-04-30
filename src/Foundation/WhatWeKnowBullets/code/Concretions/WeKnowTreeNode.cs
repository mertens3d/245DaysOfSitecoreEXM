using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.WhatWeKnowTree.Concretions
{
  public class WeKnowTreeNode : IWeKnowTreeNode
  {
    public WeKnowTreeNode(string title)
    {
      Title = title;
    }

    public WeKnowTreeNode(string title, string value)
    {
      Title = title;
      Value = value;
    }

    public bool HasLeaves
    {
      get
      {
        return Leaves != null && Leaves.Any();
      }
    }

    private List<IWeKnowTreeNode> Leaves { get; set; } = new List<IWeKnowTreeNode>();
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public bool ValueIsJson { get; set; } = false;

    public void AddNode(IWeKnowTreeNode treeNode)
    {
      if (treeNode != null)
      {
        Leaves.Add(treeNode);
      }
    }
    public void AddRawNode(string serialized)
    {
      var rawTitleLeaf = new WeKnowTreeNode("Raw");
      rawTitleLeaf.AddNode(new WeKnowTreeNode(serialized) { ValueIsJson = true });
      AddNode(rawTitleLeaf);
    }

    public string TitleValue()
    {
      var toReturn = Title;
      if (!string.IsNullOrEmpty(Value))
      {
        toReturn += " : " + Value;
      }
      return toReturn;
    }

    public List<IWeKnowTreeNode> GetLeaves()
    {
      return Leaves.Cast<IWeKnowTreeNode>().ToList();
    }

    public void AddNodes(IEnumerable<IWeKnowTreeNode> treeNodes)
    {
      Leaves.AddRange(treeNodes);
    }
  }
}