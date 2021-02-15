using Sitecore.XConnect;
using System;

namespace SitecoreCinema.Model.Collection
{
  public class BuyConcessions : Outcome
  {
    public BuyConcessions(DateTime timestamp, string currencyCode, decimal monetaryValue) : base(EventDefintionId, timestamp, currencyCode, monetaryValue) { }

    public static Guid EventDefintionId { get; } = Const.EventId.BuyConcension;
    public bool BoughtAlchoholicDrink { get; set; }
  }
}