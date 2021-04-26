using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Concretions
{
  public class WhatWeKnowTree : IWhatWeKnowTree
  {

    public WhatWeKnowTree(string rootTitle)
    {
      Root = new TreeNode(rootTitle);
    }

    public IWhatWeKnowTreeWriter TreeWriter { get {

        return new FancyTreeWriter(Root);
      } } 
    public ITreeNode Root { get; set; }
  }
}