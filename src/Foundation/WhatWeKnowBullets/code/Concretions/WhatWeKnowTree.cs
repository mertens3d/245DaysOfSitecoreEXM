using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;

namespace LearnEXM.Foundation.WhatWeKnowTree.Concretions
{
  public class WhatWeKnowTree : IWhatWeKnowTree
  {

    public WhatWeKnowTree(string rootTitle)
    {
      Root = new WeKnowTreeNode(rootTitle);
    }

    public IWhatWeKnowTreeWriter TreeWriter
    {
      get
      {

        return new FancyTreeWriter(Root);
      }
    }
    public IWhatWeKnowTreeNode Root { get; set; }
  }
}