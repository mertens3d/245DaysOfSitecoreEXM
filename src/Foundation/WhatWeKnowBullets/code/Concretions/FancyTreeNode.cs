using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Concretions
{
  public class FancyTreeNode
  {
    public FancyTreeNode(IWhatWeKnowTreeNode node)
    {
      if (node != null)
      {
        title = node.TitleValue();
        folder = node.HasLeaves;
        if (node.ValueIsJson)
        {
          extraClasses = "json-data";
        }

        if (node.HasLeaves)
        {
          foreach (var child in node.GetLeaves())
          {
            children.Add(new FancyTreeNode(child));
          }
        }
      }
    }

#pragma warning disable IDE1006 // Naming Styles
    public List<FancyTreeNode> children { get; set; } = new List<FancyTreeNode>();
    public bool folder { get; set; } = false;
    public string extraClasses { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
#pragma warning restore IDE1006 // Naming Styles
  }
}