using Feature.Marketing.Email.Models;
using LearnEXM.Feature.Marketing.Email.Models;
using System;
using System.Web.Mvc;

namespace LearnEXM.Feature.Marketing.Email.Controllers
{
  public class EmailMarketingController : Controller
  {
    private EmailMarketingControllerHelper _controllerHelper;

    private EmailMarketingControllerHelper ControllerHelper { get { return _controllerHelper ?? (_controllerHelper = new EmailMarketingControllerHelper()); } }

    public ActionResult Footer()
    {
      var viewModel = new FooterViewModel();
      return View(Const.Views.Footer, viewModel);
    }

    public ActionResult Header()
    {
      var viewModel = new HeaderViewModel();
      viewModel.LinkData = ControllerHelper.GetLinkData(viewModel.DataSource, Const.Fields.Header.Link, Const.Fields.Header.LinkTextFallBack);
      var paramDate = ControllerHelper.GetRenderingParamValue(viewModel.DataSource, Const.Fields.Header.RenderingParams.Date);
      viewModel.FormattedDate = ControllerHelper.FormatDate(paramDate, DateTime.Now);
      return View(Const.Views.Header, viewModel);
    }

    public ActionResult Hero()
    {
      var viewModel = new HeroViewModel();
      viewModel.LinkData = ControllerHelper.GetLinkData(viewModel.DataSource, Const.Fields.Hero.Link, Const.Fields.Hero.LinkTextFallBack);
      return View(Const.Views.Hero, viewModel);
    }

    public ActionResult ImageBesideText()
    {
      var viewModel = new ImageBesideTextViewModel();

      try
      {
        viewModel.LinkData = ControllerHelper.GetLinkData(viewModel.DataSource, Const.Fields.ImageBesideText.Link, Const.Fields.ImageBesideText.LinkTextFallBack);
        var backgroundColorParam = ControllerHelper.GetRenderingParamValue(viewModel.DataSource, Const.Fields.ImageBesideText.RenderingParam.BackgroundColor);
        var imageIsLeftParam = ControllerHelper.GetRenderingParamValue(viewModel.DataSource, Const.Fields.ImageBesideText.RenderingParam.ImageIsLeft);

        viewModel.BackgroundColor = ControllerHelper.GetBackgroundColor(backgroundColorParam, viewModel.BackgroundColor);
        viewModel.ImageIsLeft = !string.IsNullOrEmpty(imageIsLeftParam);
        viewModel.ImageAlignAttrValue = viewModel.ImageIsLeft ? Const.Styling.Left : Const.Styling.Right;
      }
      catch (Exception ex)
      {
        Sitecore.Diagnostics.Log.Error(Const.Diagnostics.Prefix, ex, this);
      }
      return View(Const.Views.ImageBesideText, viewModel);
    }

    public ActionResult ImageOverTextFullWidth()
    {
      var viewModel = new ImageOverTextFullWidthViewModel();
      viewModel.LinkData = ControllerHelper.GetLinkData(viewModel.DataSource, Const.Fields.ImageOverTextFullWidth.Link, Const.Fields.ImageOverTextFullWidth.LinkTextFallBack);
      return View(Const.Views.ImageOverTextFullWidth, viewModel);
    }

    public ActionResult ImageOverTextThreeColumn()
    {
      var viewModel = new ImageOverTextThreeColumnViewModel();
      viewModel.Left = new ImageOverTextSmall(viewModel.DataSource, "Left", ControllerHelper);
      viewModel.Middle = new ImageOverTextSmall(viewModel.DataSource, "Middle", ControllerHelper);
      viewModel.Right = new ImageOverTextSmall(viewModel.DataSource, "Right", ControllerHelper);
      return View(Const.Views.ImageOverTextThreeColumn, viewModel);
    }

    public ActionResult Social()
    {
      var viewModel = new SocialViewModel();
      viewModel.LinkData = ControllerHelper.GetLinkData(viewModel.DataSource, Const.Fields.ImageOverTextFullWidth.Link, Const.Fields.ImageOverTextFullWidth.LinkTextFallBack);
      return View(Const.Views.Social, viewModel);
    }
  }
}