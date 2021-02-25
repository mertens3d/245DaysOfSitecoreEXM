using System;

namespace Shared
{
  public struct Const
  {
    public struct EventId
    {
      public static Guid BuyConcension = new Guid("5A33A52E-E6EB-49F0-9F62-6F0381F1FA32");
      public static Guid UseSelfService = new Guid("1BCD1800-2376-4725-A5FB-7182550F4FCB");
      public static Guid WatchMovie = new Guid("{5144AF17-F42B-47D3-A001-D97B8340669D}");
    }

    public struct FacetKeys
    {
      public const string CinemaInfo = "CinemaInfo";
      public const string CinemaVisitorInfo = "CinemaVisitorInfo";
      //public const string DefaultFacetKey = "DefaultFacetKey";
    }

    public struct Logger
    {
      public const string CinemaPrefix = "[Sitecore Cinema] ";
    }

    public struct QueryString
    {
      public const string UserIdKey = "userid";
    }
    public struct WebSitePage
    {
      public struct SitecoreCinema
      {
        public struct Outside
        {
          public const string IsRegistered = "SitecoreCimena/Outside/Registered";
          public const string NotRegistered = "SitecoreCimena/Outside/Unknown";
        }
      }
    }
    public struct XConnect
    {
      public struct Certificate
      {
        public static string CertificateStore = "StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=";
        public static string CertificateThumbprint = "42829cff3fa746441d6faaf8e6af216c0063a685";
      }

      public struct Channels
      {
        public static Guid BoughtCandy = new Guid("{D425A958-B1BE-4EA6-BE6C-405DBEE08496}");
        public static Guid BoughtTicket = new Guid("{FD61A37C-14D6-49C8-843A-DCE74AE9147E}");
        public static Guid RegisterInteractionCode = new Guid("{557F7C69-C323-45B6-92B3-721B09DA07CF}");
        public static Guid WatchedMovie = new Guid("{37FD2F38-2324-48D0-A699-B6147EB38AFB}");
      }

      public struct CinemaId
      {
        public static int Theater22 = 22;
      }

      public struct ContactIdentifiers
      {
        public struct ExampleData
        {
          public static string MyrtleIdentifier = "myrtlesitecore9d1f652848f247219be9e1dbea4dd346";
        }

        public struct Sources
        {
          public static string SitecoreCinema = "SitecoreCinema";
          public static string Twitter = "twitter";
        }
      }

      public struct EndPoints
      {
        public static string Configuration = "https://LearnEXMxconnect.dev.local/configuration";
        public static string Odata = "https://LearnEXMxconnect.dev.local/odata";
      }

      public struct Goals
      {
        public static Guid RegistrationGoal = new Guid("{8FFB183B-DA1A-4C74-8F3A-9729E9FCFF6A}");
      }

      public struct MovieEIDR
      {
        public static string DieHard = "10.5240/08B0-F8C2-5525-BF22-BA07-4";
      }
    }
  }
}