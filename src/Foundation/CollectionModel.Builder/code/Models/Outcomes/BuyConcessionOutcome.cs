using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.Outcomes
{
  public class BuyConcessionOutcome : Outcome
  {
    public BuyConcessionOutcome(DateTime timestamp, string currencyCode, decimal monetaryValue) : base(EventDefintionId, timestamp, currencyCode, monetaryValue)
    {

      Text = "this is the Event text";
    }

    public static Guid EventDefintionId { get; } = CollectionConst.OutcomeId.BuyConcession;
    public bool BoughtAlchoholicDrink { get; set; }
  }
}