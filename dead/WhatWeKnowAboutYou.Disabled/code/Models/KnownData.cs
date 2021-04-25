using System;
using System.Collections.Generic;

namespace LearnEXM.Feature.WhatWeKnowAboutYou.Models
{
  public class KnownData
  {
    public Guid? ContactId { get; set; }
    public List<Sitecore.Analytics.Model.Entities.ContactIdentifier> Identifiers { get; set; }
    public List<InteractionProxy> KnownInteractions { get; set; }
    public bool IsKnown { get; set; }
    public KnownDataFacets FacetData { get; set; } = new KnownDataFacets();
    public string UserId { get; set; }

    public string ContactIdAsString()
    {
      string toReturn = "{unknown}";
      if (ContactId != null)
      {
        toReturn = ContactId.ToString();
      }

      return toReturn;
    }
  }
}