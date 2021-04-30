using Sitecore.Data;

namespace LearnEXM.Feature.SitecoreCinema
{
  public struct ProjectConst
  {
    public struct Logging
    {
      public static string prefix = "[LearnEXM.Feature.SitecoreCinema] ";
    }

    public struct Items
    {
      public struct Content
      {
        public static ID MovieShowTimesFolderItemID = new ID("{8F47ACF2-8BD8-4C28-AD54-C34684B82565}");
        public static ID ConcesssionsFolderItemID = new ID("{269F7CD5-2C13-47DF-B348-3DD214B77D1A}");
      }

      public struct Templates
      {
        public struct Feature
        {
          public struct SitecoreCinema
          {
            public struct MovieShowTime
            {
              //MovieShowTimeProxy
              public static ID Template = new ID("{D20A7653-E33C-4A1F-A67D-E7958CC25EE7}");
            }
            public struct MovieData
            {
              public static ID Template = new ID("{2F909BC4-EDA4-43C9-8A6D-3AC7F9C64C9C}");
              public static ID Poster = new ID("{6F6FB62C-E4F1-4781-B5D4-21D21AAE9F46}");
              public static ID MovieName = new ID("{3B14E82B-9D2A-479B-8FBC-87576BE25BFA}");
              public static ID YouTubeLink = new ID("{A71245C9-7A55-4275-AA20-3E711BE47499}");
            }

            public struct ConcessionSubCategory
            {
              public static ID Template = new ID("{44A602D7-3D90-4EC7-93BD-1A26467A69B1}");
              public static ID NameField = new ID("{FCB07E76-6B7F-4DB7-A9A3-9E57E578AD92}");

            }
            public struct ConcessionCategory
            {
              public static ID Template = new ID("{230A207E-1548-472C-A701-AC28EC5850B7}");
              public static ID IsAdultField = new ID("{C953551F-3B2E-4C92-BB0A-A75ED8FCC2F5}");
              public static ID GroupName = new ID("{C2BC8013-04D4-4014-8AEE-CB1C5B63AFA8}");
            }

            public struct ConcessionPrice
            {
              public static ID Template = new ID("{8AADEC50-24C0-4516-85B2-041B82F83CB2}");
              public static ID DescriptionField = new ID("{A9A972F8-4C88-4B28-B383-EEC332BB1BFD}");
              public static ID Price = new ID("{7162DB53-9483-4C89-8FF7-17055A6191C1}");

            }
            public struct ConcessionProduct
            {
              public static ID Template = new ID("{02E5D189-D5D6-406C-B150-CD9A3662708B}");
              public static ID ProductName = new ID("{C43E0B86-8BB7-4FDE-B3FA-6A7FE927A02E}");
              public static ID ProductLogo = new ID("{B4BFFC73-6DF0-4856-BDFC-1ACF96F2C72D}");
            }

            public struct MovieTicket
            {
              public static ID Root = new ID("{D20A7653-E33C-4A1F-A67D-E7958CC25EE7}");
              public static ID MovieNameField = new ID("{5D3E6704-362B-4AA4-92D6-0D6354CCD915}");
              public static ID MovieTimeField = new ID("{27CA3167-BDDF-42E3-948B-D017C6532198}");
            }
          }
        }
      }
    }

    public struct Links
    {
      public struct SitecoreCinema
      {
        public static string _root = "";

        public static string BuyTicket = _root + "/buy-ticket";

        public static string Landing = _root;

        public static string Register = _root + "/register";

        public static string RegisterViaForm = _root + "/registerviaform";

        public static string SelfServiceMachine = _root + "/self-service-machine";

        public struct Lobby
        {
          public static string _lobbyRoot = _root + "/Lobby";
          public static string BuyConcessions = _lobbyRoot + "/BuyConcessions";
          public static string LobbyLanding = _lobbyRoot;
          public static string WatchMovie = _lobbyRoot + "/WatchMovie";
        }
      }
    }

    public struct Assets
    {
      public static string _scriptsBase = "assets/scripts/WhatWeKnow.SitecoreCinema";
      public static string _styleBase = "assets/styles/WhatWeKnow.SitecoreCinema";
      public static string WhatWeKnowCss = _styleBase + "/WhatWeKnow.css";
      public static string ConcessionStand = _styleBase + "/ConcessionStand.css";
      public static string WhatWeKnowJs = _scriptsBase + "/WhatWeKnow.js";
    }

    public struct Views
    {
        public static string _base = "/views/feature/SitecoreCinema";
      

      public struct Concessions
      {
        public static string ConcessionStand = _base + "/_concessionStand.cshtml";
        public static string ConcessionItems = _base + "/_concessionItems.cshtml";
        public static string ConcessionCategory = _base + "/concessionCategory.cshtml";
        public static string Prices = _base + "/prices.cshtml";
      }
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