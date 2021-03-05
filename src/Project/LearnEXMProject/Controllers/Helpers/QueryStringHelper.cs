using System;
using System.Web;

namespace LearnEXMProject.Controllers.Helpers
{
  public class QueryStringHelper
  {
    public string UserId { get; set; }
    public HttpContextBase HttpContext { get; }

    public QueryStringHelper(HttpContextBase httpContext)
    {
      this.HttpContext = httpContext;
      UserId = GetUserId();
    }

    private string GetUserId()
    {
      return HttpContext.Request[Shared.Const.QueryString.UserIdKey];
    }

    public string UserIdQueryString()
    {
      return UserIdQueryString(UserId);
    }

    internal string UserIdQueryString(string idToUse)
    {
      return $"?&{Shared.Const.QueryString.UserIdKey}={idToUse}";
    }
  }
}