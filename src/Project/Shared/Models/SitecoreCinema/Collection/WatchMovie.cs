using Sitecore.XConnect;
using System;

namespace Shared.Models.SitecoreCinema.Collection
{
  public class WatchMovie : Outcome
  {
    public WatchMovie(DateTime timestamp, string currencyCode, decimal monetaryValue) : base(EventDefinitionId, timestamp, currencyCode, monetaryValue)
    {
    }

    public static Guid EventDefinitionId { get; } = new Guid("0b7e9e7e-f7ac-4ac2-8f8e-26a9c8644b23");
    public string EIDR { get; set; } // Entertainment Identifier Registry number e.g. 10.5240/24D3-956D-0575-0244-CB28-I
  }
}