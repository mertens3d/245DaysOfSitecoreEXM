using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.WhatWeKnowTree.Concretions
{
  public class WeKnowTreeNode : IWeKnowTreeNode
  {
    public WeKnowTreeNode(string title, Helpers.WeKnowTreeOptions treeOptions)
    {
      Title = title;
      TreeOptions = treeOptions;
    }

    public WeKnowTreeNode(string title, string value, Helpers.WeKnowTreeOptions treeOptions)
    {
      Title = title;
      Value = value;
      TreeOptions = treeOptions;
    }

    public bool HasLeaves
    {
      get
      {
        return Leaves != null && Leaves.Any();
      }
    }

    public string Title { get; set; } = string.Empty;
    private WeKnowTreeOptions TreeOptions { get; }
    public string Value { get; set; } = string.Empty;
    public bool ValueIsJson { get; set; } = false;
    private List<IWeKnowTreeNode> Leaves { get; set; } = new List<IWeKnowTreeNode>();

    public void AddNode(IWeKnowTreeNode treeNode)
    {
      if (treeNode != null)
      {
        Leaves.Add(treeNode);
      }
    }

    public void AddNodes(IEnumerable<IWeKnowTreeNode> treeNodes)
    {
      Leaves.AddRange(treeNodes);
    }

    public void AddRawNode(string serialized)
    {
      if (TreeOptions.IncludeRaw)
      {
        var rawTitleLeaf = new WeKnowTreeNode("Raw", TreeOptions);
        rawTitleLeaf.AddNode(new WeKnowTreeNode(serialized, TreeOptions) { ValueIsJson = true });
        AddNode(rawTitleLeaf);
      }
    }

    public List<IWeKnowTreeNode> GetLeaves()
    {
      return Leaves.Cast<IWeKnowTreeNode>().ToList();
    }

    public string TitleAndValue()
    {
      var toReturn = Title;
      if (!string.IsNullOrEmpty(Value))
      {
        toReturn += " : " + Value;
      }
      return toReturn;
    }
  }
}