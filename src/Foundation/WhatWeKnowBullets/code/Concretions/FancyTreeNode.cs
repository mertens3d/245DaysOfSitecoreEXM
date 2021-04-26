using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Concretions
{
  public class FancyTreeNode
  {
    public FancyTreeNode(ITreeNode node)
    {
      if (node != null)
      {
        this.title = node.TitleValue();
        this.folder = node.HasLeaves;
        if (node.ValueIsJson)
        {
         this. extraClasses="json-data";
        }

        if (node.HasLeaves)
        {
          foreach (var child in node.Leaves)
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