using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnEXM.Project.Website.Models
{
  public class EmailConst
  {
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
        public static string FooterLogo = "footer-logo";
        public static string NewsLetterHead = "newsletter_head";
        public static string PrimaryContent = "primary-content";
        public static string TopImage = "top-image";
      }

      public struct Views
      {
        public static string EmailBaseLayout = "_EmailBaseLayout.cshtml";
      }
    }
  }
}