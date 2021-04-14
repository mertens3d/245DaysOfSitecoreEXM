namespace LearnEXM.Feature.Marketing.Email.Models
{
  public class ImageBesideTextViewModel : _baseMarketingEmailViewModel
  {
    public string BackgroundColor { get; set; } = Const.Styling.HexColors.White;
    public string ImageAlignAttrValue { get; set; } = Const.Styling.Left;
    public bool ImageIsLeft { get; set; } = false;
    public LinkData LinkData { get; set; }
  }
}