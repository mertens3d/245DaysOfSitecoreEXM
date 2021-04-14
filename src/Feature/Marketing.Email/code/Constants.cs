using Sitecore.Data;

namespace LearnEXM.Feature.Marketing.Email
{
  public static class Const
  {
    public struct Diagnostics
    {
      public static readonly string Prefix = "[Marketing Email] ";
    }

    public struct ExperienceEditor
    {
      public static readonly string CTAInvalidURLWarning = "The associated link URL is not valid and this CTA will not display in normal mode. Edit the link URL to fix.";
    }

    public struct Fields
    {
      public struct BackgroundColor
      {
        public static ID ColorHex = new ID("{E5B30B8F-9A6C-47CE-8090-EAB71A17C568}");
      }

      public struct Footer
      {
        public static readonly string Address = "Address";
        public static readonly string Name = "Name";
        public static readonly string Text = "Text";
        public static readonly string Unsubscribe = "Unsubscribe";
        public static readonly string UpdatePreferences = "UpdatePreferences";
      }

      public struct Header
      {
        public static readonly string Bar = "Bar";
        public static readonly string Link = "Link";
        public static readonly string LinkTextFallBack = "READ MORE";
        public static readonly string Logo = "Logo";

        public struct RenderingParams
        {
          public static readonly string Date = "Date";
        }
      }

      public struct Hero
      {
        public static readonly string Image = "Image";
        public static readonly string Link = "CTA";
        public static readonly string LinkTextFallBack = "READ MORE";
        public static readonly string Text = "Text";
        public static readonly string Title = "Title";
      }

      public struct ImageBesideText
      {
        public static readonly string Image = "Image";
        public static readonly string Link = "Image Link";
        public static readonly string LinkTextFallBack = "READ MORE";
        public static readonly string Text = "Text";
        public static readonly string Title = "Title";

        public struct RenderingParam
        {
          public static readonly string BackgroundColor = "Background Color";
          public static readonly string ImageIsLeft = "Image Left";
        }
      }

      public struct ImageOverTextFullWidth
      {
        public static readonly string Image = "Image";
        public static readonly string Link = "Link";
        public static readonly string LinkTextFallBack = "READ MORE";
        public static readonly string Text = "Text";
        public static readonly string Title = "Title";
      }

      public struct ImageOverTextThreeColumn
      {
        public static readonly string ImagePrefix = "Image";
        public static readonly string LinkPrefix = "Link";
        public static readonly string LinkTextFallBack = "Connect";
        public static readonly string TextPrefix = "Text";
        public static readonly string TitlePrefix = "Title";
      }

      public struct Social
      {
        public static readonly string Link = "Link";
      }

      public struct SocialTarget
      {
        public static readonly string Icon = "Icon";
      }
    }

    public struct PlaceHolders
    {
      public static readonly string Footer = "marketing-footer";
      public static readonly string Header = "marketing-header";
      public static readonly string Hero = "marketing-hero";
      public static readonly string Main = "marketing-main";
      public static readonly string Social = "marketing-social";
    }

    public struct Styling
    {
      public static readonly string Left = "left";
      public static readonly string Right = "right";

      public struct HexColors
      {
        public const string White = "#FFFFFF";
        public static readonly string AliceBlue = "#F1F9FD";
        public static readonly string BrightBlue = "#145BAA";
        public static readonly string CobaltBlue = "#204B9F";
        public static readonly string Eclipse = "#363636";
        public static readonly string Grey = "#7E7B7B";
        public static readonly string LightGrey = "#E4E4E4";
        public static readonly string MediumGrey = "#7B8085";
        public static readonly string OuterSpace = "#303740";
        public static readonly string VividCyan = "#1AAFF2";
        public static readonly string WhiteSmoke = "#f8f8f8";
      }

      public struct TextFormat
      {
        public static readonly string Date = "MMMM d, yyyy";
      }
    }

    public struct Views
    {
      public const string _base = "/views/SitecoreCinema/MarketingEmail";
      public static readonly string Footer = _base + "/Footer.cshtml";
      public static readonly string Header = _base + "/Header.cshtml";
      public static readonly string Hero = _base + "/Hero.cshtml";
      public static readonly string ImageBesideText = _base + "/ImageBesideText.cshtml";
      public static readonly string ImageOverTextFullWidth = _base + "/ImageOverTextFullWidth.cshtml";
      public static readonly string ImageOverTextThreeColumn = _base + "/ImageOverTextThreeColumn.cshtml";
      public static readonly string Social = _base + "/Social.cshtml";

      public struct Partials
      {
        public const string _partials = _base + "/_partials";

        public struct ImageBesideText
        {
          public static readonly string _baseImageBesideText = _partials + "/ImageBesideText";
          public static readonly string ImageBesideText_Image = _baseImageBesideText + "/Image.cshtml";
          public static readonly string ImageBesideText_Text = _baseImageBesideText + "/TextAndCTA.cshtml";
        }

        public struct ImageOverTextThreeColumn
        {
          public static readonly string _threeBase = _partials + "/ImageOverTextThreeColumn";
          public static readonly string ImageTD = _threeBase + "/ImageTD.cshtml";
          public static readonly string LinkTD = _threeBase + "/LinkTD.cshtml";
          public static readonly string TextTD = _threeBase + "/TextTD.cshtml";
        }
      }
    }
  }
}