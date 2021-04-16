using Sitecore.Data;

namespace LearnEXM.Project.SitecoreCinema.Model
{
  public struct WebConst
  {
    

    public struct Views
    {
      public struct WhatWeKnowAboutYou
      {
        public static string _partials = "/views/SitecoreCinema/_partials";
        public static string facets = _partials + "/_whatWeKnowAboutYou.Facets.cshtml";
        public static string Interactions = _partials + "/_whatWeKnowAboutYou.Interactions.cshtml";

      }

    }
    public struct Items
    {
      public struct Content
      {
        public static ID MovieTicketRootItem = new ID("{8F47ACF2-8BD8-4C28-AD54-C34684B82565}");
      }

      public struct Templates
      {
        public struct Feature
        {
          public struct SitecoreCinema
          {
            public static ID MovieTicket = new ID("{D20A7653-E33C-4A1F-A67D-E7958CC25EE7}");
          }
        }
      }
    }

    public struct Links
    {
      public struct SitecoreCinema
      {
        public static string _root = "/Sitecorecinema";
        public static string BuyConcessions = Lobby + "/BuyConcessions";
        public static string BuyTicket = _root + "/buy-ticket";
        public static string Landing = _root;
        public static string Lobby = _root + "/Lobby";
        public static string Register = _root + "/register";
        public static string RegisterViaForm = _root + "/registerviaform";
        public static string SelfServiceMachine = _root + "/self-service-machine";
        public static string WatchMovie = Lobby + "/WatchMovie";
      }
    }

    public struct PlaceHolders
    {
      public static string Main = "main";
      public static string RightAside = "right-aside";
    }
  }
}