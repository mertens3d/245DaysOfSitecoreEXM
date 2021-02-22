using Sitecore.XConnect;
using System;

namespace Shared.Models.SitecoreCinema.Collection
{
  public class BuyConcessions : Outcome
  {
    public BuyConcessions(DateTime timestamp, string currencyCode, decimal monetaryValue) : base(EventDefintionId, timestamp, currencyCode, monetaryValue) { }

    public static Guid EventDefintionId { get; } = Const.EventId.BuyConcension;
    public bool BoughtAlchoholicDrink { get; set; }
  }
}