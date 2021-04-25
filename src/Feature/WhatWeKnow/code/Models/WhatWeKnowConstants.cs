namespace LearnEXM.Feature.WhatWeKnow.Models
{
  public struct WhatWeKnowConstants
  {
    public struct Views
    {
      public struct WhatWeKnow
      {
        public static string _partials = "/views/WhatWeKnow/_partials";
        public static string facets = _partials + "/_whatWeKnow.Facets.cshtml";
        public static string Interactions = _partials + "/_whatWeKnow.Interactions.cshtml";
        public static string Bullets = _partials + "/_whatWeKnow.Bullets.cshtml";
      }
    }
  }
}