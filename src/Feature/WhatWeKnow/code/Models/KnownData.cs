using System;
using System.Collections.Generic;
using Sitecore.Analytics.Model.Entities;

namespace LearnEXM.Feature.WhatWeKnow.Models
{
  public class KnownData
  {
    public Guid? ContactId { get; set; }
    public List<ContactIdentifier> Identifiers { get; set; }
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