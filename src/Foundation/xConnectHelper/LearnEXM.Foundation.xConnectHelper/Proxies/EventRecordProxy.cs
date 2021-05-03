using System;
using System.Collections.Generic;

namespace LearnEXM.Foundation.xConnectHelper.Proxies
{
  public class EventRecordProxy
  {
    public string TypeName { get;  set; }
    public Dictionary<string, string> CustomValues { get;  set; }
    public DateTime TimeStamp { get;  set; }
    public string ItemDisplayName { get;  set; }
    public Guid ItemId { get;  set; }
    public TimeSpan Duration { get;  set; }
  }
}