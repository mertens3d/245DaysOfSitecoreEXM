using LearnEXM.Feature.SitecoreCinema.Models;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using Sitecore.Analytics;
using Sitecore.XConnect.Collection.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LearnEXM.Feature.SitecoreCinema.Controllers
{
  public class WhatWeKnowController : Controller
  {
    private WhatWeKnowViewModel CommonDataHarvest(WeKnowTreeOptions options)
    {
      Sitecore.Diagnostics.Log.Debug(ProjectConst.Logging.prefix + "s) CommonDataHarvest");

      var targetFacetsTypes = new List<string>
      {
       CinemaInfo.DefaultFacetKey,
       CinemaVisitorInfo.DefaultFacetKey,
       EmailAddressList.DefaultFacetKey,
       PersonalInformation.DefaultFacetKey,
       CinemaDetails.DefaultFacetKey,
       AddressList.DefaultFacetKey,
      };

      var whatWeKnowTreeBuilder = new WhatWeKnowTreeBuilder(targetFacetsTypes, options);
      var whatWeKnowTree = whatWeKnowTreeBuilder.GetWhatWeKnowTree(Tracker.Current.Contact);
      var viewModel = new WhatWeKnowViewModel
      {
        WhatWeKnowTree = whatWeKnowTree,
      };

      Sitecore.Diagnostics.Log.Debug(ProjectConst.Logging.prefix + "e) CommonDataHarvest");

      return viewModel;
    }

    public ActionResult AsUnorderedList()
    {
      Sitecore.Diagnostics.Log.Debug(ProjectConst.Logging.prefix + "s) AsUnorderedList action");

      var options = new WeKnowTreeOptions()
      {
        IncludeRaw = false
      };

      var viewModel = CommonDataHarvest(options);

      Sitecore.Diagnostics.Log.Debug(ProjectConst.Logging.prefix + "e) AsUnorderedList action");
      return View(ProjectConst.Views.WhatWeKnow.AsUnorderedList, viewModel);
    }

    public ActionResult AsFancyTree()
    {
      Sitecore.Diagnostics.Log.Debug(ProjectConst.Logging.prefix + "s) AsFancyTree action");

      var options = new WeKnowTreeOptions();
      var viewModel = CommonDataHarvest(options);

      Sitecore.Diagnostics.Log.Debug(ProjectConst.Logging.prefix + "e) AsFancyTree action");
      return View(ProjectConst.Views.WhatWeKnow.AsFancyTree, viewModel);
    }
  }
}