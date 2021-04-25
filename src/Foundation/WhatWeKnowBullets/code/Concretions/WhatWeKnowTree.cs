using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Concretions
{
  public class WhatWeKnowTree : IWhatWeKnowTree
  {

    public IWhatWeKnowTreeWriter TreeWriter { get {

        return new FancyTreeWriter(Root);
      } } 
    public ITreeNode Root { get; set; } = new TreeNode("root");
  }
}