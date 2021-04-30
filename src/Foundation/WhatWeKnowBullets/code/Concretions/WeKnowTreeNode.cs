using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.WhatWeKnowTree.Concretions
{
  public class WeKnowTreeNode : IWhatWeKnowTreeNode
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

    private List<IWhatWeKnowTreeNode> Leaves { get; set; } = new List<IWhatWeKnowTreeNode>();
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public bool ValueIsJson { get; set; } = false;

    public void AddNode(IWhatWeKnowTreeNode treeNode)
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

    public List<IWhatWeKnowTreeNode> GetLeaves()
    {
      return Leaves.Cast<IWhatWeKnowTreeNode>().ToList();
    }

    public void AddNodes(IEnumerable<IWhatWeKnowTreeNode> treeNodes)
    {
      Leaves.AddRange(treeNodes);
    }
  }
}