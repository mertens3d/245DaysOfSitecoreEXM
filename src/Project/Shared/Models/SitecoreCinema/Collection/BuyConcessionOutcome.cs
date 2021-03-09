using Sitecore.XConnect;
using System;

namespace Shared.Models.SitecoreCinema.Collection
{
  public class BuyConcessionOutcome : Outcome
  {
    public BuyConcessionOutcome(DateTime timestamp, string currencyCode, decimal monetaryValue) : base(EventDefintionId, timestamp, currencyCode, monetaryValue) {

      Text = "this is the Event text";
    }

    public static Guid EventDefintionId { get; } = Const.OutcomeId.BuyConcession;
    public bool BoughtAlchoholicDrink { get; set; }
  }
}