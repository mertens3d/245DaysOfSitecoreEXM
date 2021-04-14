using System;

namespace LearnEXM.Foundation.Marketing
{
  public struct MarketingConst
  {
    public struct ContactType
    {
      public const string ContactTypeA = "ContactTypeA";
      public const string ContactTypeB = "ContactTypeB";
      public const string ContactTypeC = "ContactTypeC";
      public const string ContactTypeD = "ContactTypeD";
    }

    public struct Divisions
    {
      public const string DivisionA = "DivisionA";
      public const string DivisionB = "DivisionB";
      public const string DivisionC = "DivisionC";
    }

    public struct FacetKeys
    {
      public const string Marketing = "Marketing";
    }

    public struct Models
    {
      public static string MarketingFacetModel = "MarketingFacetModel";
    }

    public struct XConnect
    {
      public struct AddressListKeys
      {
        public const string Marketing = "Marketing";
      }

      public struct Channels
      {
        public static Guid MockContactRegistration = new Guid("{}");
      }

      public struct ContactIdentifiers
      {
        public struct Sources
        {
          public static string Marketing = "Marketing";
        }
      }

      public struct EmailKeys
      {
        public const string Work = "Work";
      }

      public struct EndPoints
      {
        public static string _localRoot = "https://LearnEXMe.xconnect";
        public static string Configuration = _localRoot + "/configuration";
        public static string Odata = _localRoot + "/odata";
      }

      public struct Genders
      {
        public static string Female = "Female";
        public static string Male = "Male";
      }

      public struct Goals
      {
        public static Guid MockContactRegistered = new Guid("{}");
      }

      public struct PostalCodes
      {
        public static string US = "1";
      }
    }
  }
}