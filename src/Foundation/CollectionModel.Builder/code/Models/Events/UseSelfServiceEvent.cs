using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.Events
{
  public class UseSelfServiceEvent : Event
  {
    public static Guid EventDefinitionId { get; } = CollectionConst.EventId.UseSelfService;

    public UseSelfServiceEvent(DateTime timeStamp) : base(EventDefinitionId, timeStamp)
    {
    }
  }
}