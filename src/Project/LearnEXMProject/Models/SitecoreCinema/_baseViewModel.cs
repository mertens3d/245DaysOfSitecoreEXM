using LearnEXMProject.Controllers.Helpers;
using System.Collections.Generic;
using System.Web;

namespace LearnEXMProject.Models.SitecoreCinema
{
  public abstract class _baseViewModel
  {
    public _baseViewModel()
    {
      QueryStringHelper = new QueryStringHelper(new HttpContextWrapper( HttpContext.Current));
    }

    public string UserId { get; set; }
    public List<string> XConnectErrors { get; set; }
    public QueryStringHelper QueryStringHelper { get; }

    public string UserIdQueryString()
    {
      return QueryStringHelper.UserIdQueryString(UserId);
      //return $"?&{Shared.Const.QueryString.UserIdKey}=" + UserId;
    }
  }
}