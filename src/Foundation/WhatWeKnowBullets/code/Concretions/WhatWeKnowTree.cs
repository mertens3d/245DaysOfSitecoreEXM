using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using System.Diagnostics;

namespace LearnEXM.Foundation.WhatWeKnowTree.Concretions
{
  public class WhatWeKnowTree : IWhatWeKnowTree
{

    public WhatWeKnowTree(string rootTitle, WeKnowTreeOptions TreeOptions)
    {
      Root = new WeKnowTreeNode(rootTitle, TreeOptions);
    }

    public IWhatWeKnowTreeWriter TreeWriter
    {
      get
      {

        return new FancyTreeWriter(Root);
      }
    }
    public IWeKnowTreeNode Root { get; set; }
  }
}