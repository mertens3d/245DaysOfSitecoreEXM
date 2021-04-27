﻿using LearnEXM.Feature.SitecoreCinema;
using LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models;
using LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models.BulletFactories;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Helpers;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
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
       CinemaDetails.DefaultFacetKey
      };

      var customFacetKeyBulletFactories = new List<IFacetNodeFactory>()
      {
         new CinemaInfoTreeNodeFactory(),
         new CinemaVisitorInfoFacetTreeNodeFactory(),
         new CinemaDetailsFacetTreeNodeFactory()
      };

      var knownDataHelper = new KnownDataHelper(targetFacetsTypes, customFacetKeyBulletFactories);

      KnownData knownDataViaTracker = knownDataHelper.GetKnownDataViaTracker(Tracker.Current.Contact);

      var viewModel = new WhatWeKnowViewModel
      {
        KnownData = knownDataViaTracker,
      };

      return View(ProjectConst.Views.WhatWeKnow.Main, viewModel);
    }
  }
}