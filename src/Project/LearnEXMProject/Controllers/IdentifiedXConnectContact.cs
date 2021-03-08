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

      //if (filtercontext.HttpContext.Request != null)
      //{
      //  var UserId = new QueryStringHelper(filtercontext.HttpContext).UserId;

      //  if (!string.IsNullOrEmpty(UserId))
      //  {
      //    if (Tracker.Current != null && Tracker.Current.Contact.IdentificationLevel != Sitecore.Analytics.Model.ContactIdentificationLevel.Known)
      //    {
      //      try
      //      {
      //        Tracker.Current.Session.IdentifyAs(Shared.Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, UserId);
      //        toReturn = true;
      //      }
      //      catch (System.Exception ex)
      //      {

      //        Sitecore.Diagnostics.Log.Error(UserId, this);
      //        Sitecore.Diagnostics.Log.Error(ex.Message, this);
      //      }
            
      //    }

      //  }
      //}
          toReturn = Tracker.Current.Contact.IdentificationLevel == Sitecore.Analytics.Model.ContactIdentificationLevel.Known;

      return toReturn;
    }
  }
}