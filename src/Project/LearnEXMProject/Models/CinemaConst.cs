using Sitecore.Data;
using System;

namespace LearnEXMProject.Models
{
  public struct CinemaConst
  {
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
        private static string root = "/Sitecorecinema";
        public static string SelfServiceMachine = root + "/self-service-machine";
        public static string BuyTicket = root + "/buy-ticket";
        public static string Landing = root;
        public static string Register = root + "/register";
        public static string RegisterViaForm = root + "/registerviaform";

        public static string Lobby = root + "/Lobby";
        public static string BuyConcessions = Lobby + "/BuyConcessions";
        public static string WatchMovie = Lobby + "/WatchMovie";
      }
    }

    public struct PlaceHolders
    {
      public static string Main = "main";
      public static string RightAside = "right-aside";
    }

    public struct EXM
    {
      public struct Colors
      {
        public static string ColorF7F2E7 = "#f7f2e7";
      }

      public struct Fields
      {
        public static string PageTitle = "Page Title";
      }

      public struct HTML
      {
        public static string MinTableAttributes = "cellpadding='0' cellspacing='0' border='0' style='border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt;'";
      }

      public struct PlaceHolders
      {
        public static string BottomImage = "bottom-image";
        public static string EmailLogo = "email-logo";
        public static string NewsLetterHead = "newsletter_head";
        public static string PrimaryContent = "primary-content";
        public static string TopImage = "top-image";
        public static string FooterLogo = "footer-logo";
      }

      public struct Views
      {
        public static string EmailBaseLayout = "_EmailBaseLayout.cshtml";
      }
    }
  }
}