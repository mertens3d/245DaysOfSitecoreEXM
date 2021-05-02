namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers
{
  public class WeKnowTreeOptions {
    public bool IncludeRaw { get; set; } = true;
    public bool IncludeLastModified { get; set; } = true;
    public bool IncludeNullValueItems { get; set; } = true;
    public bool IncludeFacets { get; set; } = true;
    public bool IncludeInteractions { get; set; } = true;
    public bool IncludeIdentifiers { get; set; } = true;
    public bool IncludeTrackingContact { get; set; } = true;
    public bool IncludeEvents { get; set; } = true;
  }
}