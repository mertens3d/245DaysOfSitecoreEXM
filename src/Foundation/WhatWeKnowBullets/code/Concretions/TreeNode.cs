using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Concretions
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

    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public List<ITreeNode> Leaves { get; set; } = new List<ITreeNode>();

    public bool HasLeaves
    {
      get
      {
        return Leaves != null && Leaves.Any();
      }
    }

    //public StringBuilder DrawNodeForFancyTree()
    //{
    //  var toReturn = new StringBuilder();

    //  var title = TitleValue();

    //  toReturn.Append("{title:\"" + title + "\", folder: " + HasLeaves.ToString().ToLower());

    //  foreach (var leaf in Leaves)
    //  {
    //  var fancyTreeNode = new FancyTreeNode(leaf)

    //  }


    //  if (HasLeaves)
    //  {
    //    toReturn.Append(", children: [");
    //    foreach (var leaf in Leaves)
    //    {
    //      toReturn.Append(leaf.DrawNodeForFancyTree());
    //    }
    //    toReturn.Append("]");
    //  }

    //  toReturn.Append("}");

    //  return toReturn;
    //}

    public string TitleValue()
    {
      var toReturn = Title;
      if (!string.IsNullOrEmpty(Value))
      {
        Title += " : " + Value;
      }
      return toReturn;
    }
  }
}