using LearnEXM.Feature.SitecoreCinema.Models;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.Extensions;
using LearnEXM.Foundation.WeKnowTree.Helpers;
using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using Sitecore.Analytics;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LearnEXM.Feature.SitecoreCinema.Controllers
{
  public class WhatWeKnowController : Controller
  {
    private WeKnowViewModel CommonDataHarvest(WeKnowTreeOptions weKnowTreeOptions)
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

      weKnowTreeOptions.TargetedFacetKeys.AddRange(targetFacetsTypes);

      var whatWeKnowTreeBuilder = new WeKnowTreeBuilder(weKnowTreeOptions);//.targetFacetsTypes, options);
      var whatWeKnowTree = whatWeKnowTreeBuilder.GetWeKnowTreeFromTrackingContact(Tracker.Current.Contact);
      var viewModel = new WeKnowViewModel
      {
        WeKnowTree = whatWeKnowTree,
      };

      Sitecore.Diagnostics.Log.Debug(ProjectConst.Logging.prefix + "e) CommonDataHarvest");

      return viewModel;
    }

    public ActionResult XLogy(Guid? contactId, Guid? optionsId )
    {

      var options = new WeKnowTreeOptions()
      {
        IncludeRaw = false
      };

      var viewModel = CommonDataHarvest(options);

      return View(ProjectConst.Views.WhatWeKnow.XLogy, viewModel);
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
      var dataSourceStr = Sitecore.Mvc.Presentation.RenderingContext.CurrentOrNull.Rendering.DataSource;
      Item dataSource = null;

      if (!string.IsNullOrEmpty(dataSourceStr))
      {
        dataSource = Sitecore.Context.Database.GetItem(new ID(dataSourceStr));
      }
      else
      {
        Sitecore.Diagnostics.Log.Warn(ProjConstants.Logger.LoggingPrefix + "null or empty datasource", this);
      }
      var options = new WeKnowTreeOptionsFactory().GetWeKnowTreeOptions(dataSource);
      var viewModel = CommonDataHarvest(options);

      Sitecore.Diagnostics.Log.Debug(ProjectConst.Logging.prefix + "e) AsFancyTree action");
      return View(ProjectConst.Views.WhatWeKnow.AsFancyTree, viewModel);
    }
  }
}