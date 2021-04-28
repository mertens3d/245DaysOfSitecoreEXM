using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using LearnEXM.Foundation.xConnectHelper.Proxies;
using System;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Concretions
{
  public class KnownData
  {
    public KnownData(string rootTitle)
    {
      WhatWeKnowTree = new WhatWeKnowTree(rootTitle);
    }

    public IWhatWeKnowTree WhatWeKnowTree { get; set; }

    
  }
}