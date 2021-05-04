using Sitecore.Data.Items;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers
{
  public class WeKnowTreeOptionsInteractions
  {
    public bool IncludeInteractions { get; set; } = true;
    public bool IncludeInteractionEvents { get; set; } = true;
    public List<Item> ChannelFilters { get; set; } = new List<Item>();
  }
}