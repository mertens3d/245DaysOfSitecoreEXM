using Sitecore.Analytics;
using System.Web.Mvc;

namespace LearnEXM.Project.SitecoreCinema.MVCHelpers
{
  public class IdentifiedXConnectContact : FilterAttribute, IActionFilter
  {
    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
      //empty
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
      var passes = false;

      if (Tracker.Current?.Contact != null)
      {
        if (Tracker.Current.Contact.IdentificationLevel == Sitecore.Analytics.Model.ContactIdentificationLevel.Known)
        {
          passes = true;
        }
      }

      if (!passes)
      {
        filterContext.Controller.ControllerContext.HttpContext.Response.Redirect(Feature.SitecoreCinema.ProjectConst.Links.SitecoreCinema.Landing);
      }
    }
  }
}