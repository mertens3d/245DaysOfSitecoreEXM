using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;

namespace LearnEXM.Feature.ExperienceProfileFacetTab.Models
{
  public class EpExpressCinemaVisitorInfoModel
  {
    public string ContactId { get; set; }
    public string FavoriteMovie { get; internal set; }
    public IWeKnowTree WeKnowTree { get; set; }
  }
}