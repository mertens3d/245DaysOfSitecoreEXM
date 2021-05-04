namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers
{
  public class WeKnowTreeOptions
  {
    public bool IncludeFacets { get; set; } = true;
    public bool IncludeIdentifiers { get; set; } = true;
    public bool IncludeLastModified { get; set; } = true;
    public bool IncludeNullAndEmptyValueLeaves { get; set; } = false;
    public bool IncludeNullValueItems { get; set; } = true;
    public bool IncludeRaw { get; set; } = true;
    public bool IncludeTrackingContact { get; set; } = true;
    public WeKnowTreeOptionsInteractions Interactions { get; set; } = new WeKnowTreeOptionsInteractions();
  }
}