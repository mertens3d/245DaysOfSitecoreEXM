using LearnEXM.Feature.WhatWeKnowAboutYou.Helpers;
using LearnEXM.Feature.WhatWeKnowAboutYou.Models;
using Sitecore.Analytics;
using Sitecore.Analytics.Tracking;
using System.Web.Mvc;

namespace LearnEXM.Feature.WhatWeKnowAboutYou.Controllers
{
  public class WhatWeKnowAboutYouController : Controller
  {
    public ActionResult WhatWeKnowAboutYou()
    {
      Contact trackingContact = Tracker.Current.Contact;

      var knownDataHelper = new KnownDataHelper();

      KnownData knownDataXConnect = null;

      //Task.Run(async () =>
      //{
      //  knownDataXConnect = await knownDataHelper.GetKnownDataByIdentifierViaXConnect(QueryStringHelper.UserId);
      //}
      //).Wait();

      //KnownData knownDataViaTracker = null;// knownDataHelper.GetKnownDataViaTracker(trackingContact);
      KnownData knownDataViaTracker = knownDataHelper.GetKnownDataViaTracker(trackingContact);

      //Tracker.Current.Contact // <--- Use this
      // use other Contact outside of a web page.

      //knownDataTracker.IsNew = trackingContact.IsNew;

      if (knownDataXConnect != null)
      {
        knownDataHelper.AppendCurrentContextData(knownDataXConnect, Sitecore.Context.Database);
      }

      var viewModel = new WhatWeKnowAboutYouViewModel
      {
        KnownDataXConnect = knownDataViaTracker,
        KnownDataTracker = null
      };
      return View(viewModel);
    }
  }
}