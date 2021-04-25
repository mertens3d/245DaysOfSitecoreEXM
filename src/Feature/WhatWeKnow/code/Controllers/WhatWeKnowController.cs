using LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models;
using LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Models.BulletFactories;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Helpers;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using Sitecore.Analytics;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LearnEXM.Feature.WhatWeKnow.SitecoreCinema.Controllers
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

      var customFacetKeyBulletFactories = new List<IFacetBulletFactory>()
      {
         new CinemaInfoBulletFactory(),
         new CinemaVisitorInfoBulletFactory(),
         new CinemaDetailsBulletFactory()
      };

      var knownDataHelper = new KnownDataHelper(targetFacetsTypes, customFacetKeyBulletFactories);

      KnownData knownDataXConnect = null;

      KnownData knownDataViaTracker = knownDataHelper.GetKnownDataViaTracker(Tracker.Current.Contact);

      if (knownDataXConnect != null)
      {
        knownDataHelper.AppendCurrentContextData(knownDataXConnect, Sitecore.Context.Database);
      }

      var viewModel = new WhatWeKnowViewModel
      {
        KnownDataXConnect = knownDataViaTracker,
        KnownDataTracker = null
      };
      return View(WhatWeKnowConstants.Views.WhatWeKnow.Main, viewModel);
    }
  }
}