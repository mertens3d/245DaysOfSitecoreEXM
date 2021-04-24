﻿using LearnEXM.Project.SitecoreCinema.Model;
using Sitecore.Analytics;
using System.Web.Mvc;

namespace LearnEXM.Project.SitecoreCinema.Controllers
{
  public class IdentifiedXConnectContact : FilterAttribute, IActionFilter
  {
    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
      //empty
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (Tracker.Current.Contact.IdentificationLevel != Sitecore.Analytics.Model.ContactIdentificationLevel.Known)
      {
        filterContext.Controller.ControllerContext.HttpContext.Response.Redirect(WebConst.Links.SitecoreCinema.Landing);
      }
    }
  }
}