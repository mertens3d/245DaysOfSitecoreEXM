using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Foundation.WhatWeKnowTree.Concretions
{
  public class TreeNode : ITreeNode
  {
    public TreeNode(string title)
    {
      Title = title;
    }

    public TreeNode(string title, string value)
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

    private List<ITreeNode> Leaves { get; set; } = new List<ITreeNode>();
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public bool ValueIsJson { get; set; } = false;

    public void AddNode(ITreeNode treeNode)
    {
      if (treeNode != null)
      {
        Leaves.Add(treeNode);
      }
    }
    public void AddRawNode(string serialized)
    {
      var rawTitleLeaf = new TreeNode("Raw");
      rawTitleLeaf.AddNode(new TreeNode(serialized) { ValueIsJson = true });
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

    public List<ITreeNode> GetLeaves()
    {
      return Leaves.Cast<ITreeNode>().ToList();
    }

    public void AddNodes(IEnumerable<ITreeNode> treeNodes)
    {
      Leaves.AddRange(treeNodes);
    }
  }
}