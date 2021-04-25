namespace LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models
{
  public struct WhatWeKnowConstants
  {
    public struct Views
    {
      public struct WhatWeKnow
      {
        public static string _base = "/views/Feature/WhatWeKnow.SitecoreCinema";
        public static string _partials = _base +  "/_partials";
        public static string Main = _base + "/whatWeKnow.cshtml";
        public static string Bullet = _partials + "/_whatWeKnow.Bullet.cshtml";
        public static string facets = _partials + "/_whatWeKnow.Facets.cshtml";
        public static string Interactions = _partials + "/_whatWeKnow.Interactions.cshtml";
      }
    }
  }
}