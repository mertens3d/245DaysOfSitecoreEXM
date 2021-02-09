using System;

namespace Sitecore.Documentation
{
  public struct Const
  {
    public struct XConnect
    {
      public struct ContactIdentifiers
      {
        public struct Sources
        {
          public static string Twitter = "twitter";
        }
      }

      public struct Goals
      {
        public static Guid WatchedDemo = Guid.Parse("ad8ab7fe-ab48-4ea9-a976-ae7a268ae2f0");
      }
       public struct ChannelIds
      {
        public static Guid OtherEvent = Guid.Parse( "110cbf07-6b1a-4743-a398-6749acfcd7aa");
      }

      public struct Certificate
      {
        public static string CertificateStore = "StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=";
        public static string CertificateThumbprint = "42829cff3fa746441d6faaf8e6af216c0063a685";
      }

      public struct EndPoints
      {
        public static string Odata = "https://LearnEXMxconnect.dev.local/odata";
        public static string Configuration = "https://LearnEXMxconnect.dev.local/configuration";
      }
    }
  }
}