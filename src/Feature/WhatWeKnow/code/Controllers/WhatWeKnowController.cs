using LearnEXM.Feature.SitecoreCinema.Models;
using LearnEXM.Feature.SitecoreCinema.Models.TreeNodeFactories;
using LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models;
using LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models.BulletFactories;
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
      var targetFacetsTypes = new List<string>
      {
       CinemaInfo.DefaultFacetKey,
       CinemaVisitorInfo.DefaultFacetKey,
       EmailAddressList.DefaultFacetKey,
       PersonalInformation.DefaultFacetKey,
       CinemaDetails.DefaultFacetKey,
       AddressList.DefaultFacetKey,
      };

      var customFacetKeyBulletFactories = new List<IFacetNodeFactory>()
      {
         new CinemaInfoTreeNodeFactory(),
         new CinemaVisitorInfoFacetTreeNodeFactory(),
         new CinemaDetailsFacetTreeNodeFactory()
      };

      var trackingHelper = new TrackingHelper(targetFacetsTypes, customFacetKeyBulletFactories);

      var whatWeKnowTreeBuilder = new WhatWeKnowTreeBuilder(targetFacetsTypes, customFacetKeyBulletFactories);

      var whatWeKnowTree = whatWeKnowTreeBuilder.GetWhatWeKnowTree(Tracker.Current.Contact);

      var viewModel = new WhatWeKnowViewModel
      {
        WhatWeKnowTree = whatWeKnowTree,
      };

      return View(ProjectConst.Views.WhatWeKnow.Main, viewModel);
    }
  }
}