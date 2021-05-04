using Sitecore.Data;

namespace LearnEXM.Foundation.WhatWeKnowTree
{
  public struct ProjConstants
  {
    public struct Items
    {
      public struct Templates
      {
        public struct WeKnowTreeOptions
        {
          public static ID Root = new ID("{A669E427-ECA3-48B6-8F7C-8D5C2D373AAD}");

          public struct Interactions
          {
            public static ID IncludeInteractionEvents = new ID("{798FE167-58A7-4134-ADED-A770C19EA66C}");
            public static ID IncludeInteractions = new ID("{CDDD572C-9A59-4F0A-AD26-88829AABD28D}");
            public static ID ChannelFilter = new ID("{5D277163-578E-4D57-83BB-92402F990D32}");
          }

          public static ID IncludeFacetsField = new ID("{F425937A-09A1-460E-8245-7C4C9A3DFCF5}");
          public static ID IncludeIdentifiers = new ID("{25E2BB7B-C0AA-4623-8BC3-6D118FB9955E}");
          public static ID IncludeLeavesWithNullOrEmptyValues = new ID("{7EB7D037-CED1-43C4-A2F3-5741D1C9409C}");
          public static ID IncludeRaw = new ID("{FD359CBF-6FB9-49DF-BF11-93078B9838B0}");
          public static ID IncludeTrackingContact = new ID("{7E765EF2-165F-435F-BA4D-866F4CD1DCE3}");
          public static ID IncludeLastModified = new ID("{F8F22460-65D0-48A5-8F5D-70A9ECD6733A}");
        }
      }
    }

    public struct Logger
    {
      public static string Prefix = "[LearnEXM.Foundation.WhatWeKnowTree] ";
    }
  }
}