using LearnEXMProject.Controllers.Helpers;
using LearnEXMProject.Models;
using Sitecore.Analytics;
using System.Web.Mvc;

namespace LearnEXMProject.Controllers
{
  public class IdentifiedXConnectContact : FilterAttribute, IActionFilter
  {
    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
      //empty
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (!IdentifyUser(filterContext))
      {
        filterContext.Controller.ControllerContext.HttpContext.Response.Redirect(CinemaConst.Links.SitecoreCinema.Landing);
      }
    }

    private bool IdentifyUser(ActionExecutingContext filtercontext)
    {
      bool toReturn = false;

      if (filtercontext.HttpContext.Request != null)
      {
        var UserId = new QueryStringHelper(filtercontext.HttpContext).UserId;

        if (!string.IsNullOrEmpty(UserId))
        {
          Tracker.Current.Session.IdentifyAs(Shared.Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, UserId);
          toReturn = true;
        }
      }

      return toReturn;
    }
  }
}