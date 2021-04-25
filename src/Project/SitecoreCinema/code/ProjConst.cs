using Sitecore.Data;

namespace LearnEXM.Project.SitecoreCinema
{
  public struct ProjConst
  {
    public struct Items
    {
      public struct Content
      {
        public static ID MovieShowTimesFolderItem = new ID("{8F47ACF2-8BD8-4C28-AD54-C34684B82565}");
      }

      public struct Templates
      {
        public struct Feature
        {
          public struct SitecoreCinema
          {
            public struct MovieData
            {
              public static ID Poster = new ID("{6F6FB62C-E4F1-4781-B5D4-21D21AAE9F46}");
              public static ID MovieName = new ID("{3B14E82B-9D2A-479B-8FBC-87576BE25BFA}");
              public static ID YouTubeLink = new ID("{A71245C9-7A55-4275-AA20-3E711BE47499}");
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

    public struct PlaceHolders
    {
      public static string Main = "main";
      public static string RightAside = "right-aside";
    }

    public struct Views
    {
    }
  }
}