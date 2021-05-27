using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using System.Diagnostics;

namespace LearnEXM.Foundation.WhatWeKnowTree.Concretions
{
  public class WhatWeKnowTree : IWeKnowTree
{

    public WhatWeKnowTree(string rootTitle, WeKnowTreeOptions TreeOptions)
    {
      Root = new WeKnowTreeNode(rootTitle, TreeOptions);
    }

    public IWeKnowTreeWriter TreeWriter
    {
      get
      {

        return new FancyTreeWriter(Root);
      }
    }
    public IWeKnowTreeNode Root { get; set; }
  }
}