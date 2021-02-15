using Sitecore.XConnect;
using System;

namespace SitecoreCinema.Model.Collection
{
  public class UseSelfService : Event
  {
    public static Guid EventDefinitionId { get; } = Const.EventId.UseSelfService;

    public UseSelfService(DateTime timeStamp) : base(EventDefinitionId, timeStamp) { }

  }
}