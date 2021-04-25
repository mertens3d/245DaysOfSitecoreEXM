namespace LearnEXM.Feature.WhatWeKnow.SitecoreCinema
{
  public struct ProjectConst
  {
    public struct Assets
    {
      public static string _scriptsBase = "assets/scripts/WhatWeKnow.SitecoreCinema";
      public static string _styleBase = "/styles/WhatWeKnow.SitecoreCinema";
      public static string WhatWeKnowCss = _styleBase + "/WhatWeKnow.css";
      public static string WhatWeKnowJs = _scriptsBase + "/WhatWeKnow.js";

    }
    public struct Views
    {
      public struct WhatWeKnow
      {
        public static string _base = "/views/Feature/WhatWeKnow.SitecoreCinema";
        public static string _partials = _base + "/_partials";
        public static string Main = _base + "/whatWeKnow.cshtml";
        public static string Bullet = _partials + "/_whatWeKnow.Bullet.cshtml";
        public static string facets = _partials + "/_whatWeKnow.Facets.cshtml";
        public static string Interactions = _partials + "/_whatWeKnow.Interactions.cshtml";
      }
    }
  }
}