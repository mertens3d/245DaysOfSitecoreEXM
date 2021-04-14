namespace LearnEXM.Feature.Marketing.Email.Models
{
  public class LinkData
  {
    public string LinkText { get; set; } = string.Empty;
    public bool LinkUrlIsValid { get; set; } = false;
    public bool ShowLink { get { return LinkUrlIsValid || Sitecore.Context.PageMode.IsExperienceEditor; } }
  }
}
