using System;

namespace Shared
{
  public struct Const
  {
    public struct XConnect
    {
      public struct MovieEIDR
      {

        public static string DieHard = "10.5240/08B0-F8C2-5525-BF22-BA07-4";

      }
      public struct CinemaId
      {
        
        public static int Theater22 = 22;
      }
      public struct Certificate
      {
        public static string CertificateStore = "StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=";
        public static string CertificateThumbprint = "42829cff3fa746441d6faaf8e6af216c0063a685";
      }

      public struct Channels
      {
        public static Guid BoughtTicket =  new Guid("{FD61A37C-14D6-49C8-843A-DCE74AE9147E}");
        public static Guid BoughtCandy =  new Guid("{D425A958-B1BE-4EA6-BE6C-405DBEE08496}");
        public static Guid WatchedMovie =  new Guid("{37FD2F38-2324-48D0-A699-B6147EB38AFB}");
      }
      public struct EndPoints
      {
        public static string Odata = "https://LearnEXMxconnect.dev.local/odata";
        public static string Configuration = "https://LearnEXMxconnect.dev.local/configuration";
      }

      public struct ContactIdentifiers
      {
        public struct ExampleData
        {
          public static string MyrtleIdentifier = "myrtlesitecore9d1f652848f247219be9e1dbea4dd346";
        }

        public struct Sources
        {
          public static string Twitter = "twitter";
          public static string SitecoreCinema = "SitecoreCinema";
        }
      }
    }
  }
}