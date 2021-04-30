using LearnEXM.Feature.SitecoreCinema.Models;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Sitecore.Analytics;
using Sitecore.XConnect.Collection.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LearnEXM.Feature.SitecoreCinema.Controllers
{
  public class WhatWeKnowController : Controller
  {
    public ActionResult WhatWeKnow()
    {
      Sitecore.Diagnostics.Log.Debug(ProjectConst.Logging.prefix + "s) WhatWeKnow action");

      var targetFacetsTypes = new List<string>
      {
       CinemaInfo.DefaultFacetKey,
       CinemaVisitorInfo.DefaultFacetKey,
       EmailAddressList.DefaultFacetKey,
       PersonalInformation.DefaultFacetKey,
       CinemaDetails.DefaultFacetKey,
       AddressList.DefaultFacetKey,
      };


      var whatWeKnowTreeBuilder = new WhatWeKnowTreeBuilder(targetFacetsTypes);

      var whatWeKnowTree = whatWeKnowTreeBuilder.GetWhatWeKnowTree(Tracker.Current.Contact);

      var viewModel = new WhatWeKnowViewModel
      {
        WhatWeKnowTree = whatWeKnowTree,
      };

      Sitecore.Diagnostics.Log.Debug(ProjectConst.Logging.prefix + "e) WhatWeKnow action");
      return View(ProjectConst.Views.WhatWeKnow.Main, viewModel);
    }
  }
}