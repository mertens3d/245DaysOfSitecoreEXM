using System.Collections.Generic;

namespace LearnEXM.Feature.WhatWeKnowAboutYou.Models
{
  public class EventRecordProxy
  {
    public string TypeName { get; internal set; }
    public Dictionary<string, string> CustomValues { get; internal set; }
  }
}