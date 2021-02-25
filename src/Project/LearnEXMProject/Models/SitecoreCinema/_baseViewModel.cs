using System.Collections.Generic;

namespace LearnEXMProject.Models.SitecoreCinema
{
  public abstract class _baseViewModel
  {
    public string UserId { get; set; }
    public List<string> XConnectErrors { get; set; }
    public string UserIdQueryString()
    {
      return $"?&{Shared.Const.QueryString.UserIdKey}=" + UserId;
    }
  }
}