using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using LearnEXM.Foundation.xConnectHelper.Proxies;
using System;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Concretions
{
  public class KnownData
  {
    public KnownData(string rootTitle)
    {
      this.WhatWeKnowTree = new WhatWeKnowTree(rootTitle);
    }

    public Guid? ContactId { get; set; }
    public List<InteractionProxy> KnownInteractions { get; set; }
    public bool IsKnown { get; set; }
    public IWhatWeKnowTree WhatWeKnowTree { get; set; }
    public string UserId { get; set; }

    public string ContactIdAsString()
    {
      string toReturn = "{unknown}";
      if (ContactId != null)
      {
        toReturn = ContactId.ToString();
      }

      return toReturn;
    }
  }
}