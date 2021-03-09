using Sitecore.XConnect;
using System;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.Outcomes
{
  public class WatchMovieOutcome : Outcome
  {
    public WatchMovieOutcome(DateTime timestamp, string currencyCode, decimal monetaryValue) : base(EventDefinitionId, timestamp, currencyCode, monetaryValue)
    {
    }
    public static Guid EventDefinitionId { get; } = CollectionConst.EventId.WatchMovie;
    public string EIDR { get; set; } // Entertainment Identifier Registry number e.g. 10.5240/24D3-956D-0575-0244-CB28-I
  }
}