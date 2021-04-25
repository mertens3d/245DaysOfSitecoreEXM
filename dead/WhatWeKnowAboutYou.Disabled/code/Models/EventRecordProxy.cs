using System;
using System.Collections.Generic;

namespace LearnEXM.Feature.WhatWeKnowAboutYou.Models
{
  public class EventRecordProxy
  {
    public string TypeName { get; internal set; }
    public Dictionary<string, string> CustomValues { get; internal set; }
    public DateTime TimeStamp { get; internal set; }
    public string ItemDisplayName { get; internal set; }
    public Guid ItemId { get; internal set; }
    public TimeSpan Duration { get; internal set; }
  }
}