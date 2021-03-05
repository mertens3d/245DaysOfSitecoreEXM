using LearnEXMProject.Controllers.Helpers;
using LearnEXMProject.Models;
using LearnEXMProject.Models.SitecoreCinema;
using Shared.Models;
using Shared.XConnect.Helpers;
using Shared.XConnect.Interactions;
using Sitecore.Analytics;
using Sitecore.Analytics.Tracking;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LearnEXMProject.Controllers
{
  public class SitecoreCinemaController : Controller
  {
    private QueryStringHelper _queryStringHelper;

    public SitecoreCinemaController()
    {
    }

    public QueryStringHelper QueryStringHelper
    {
      get
      {
        return _queryStringHelper ?? (_queryStringHelper = new QueryStringHelper(HttpContext));
      }
    }

    [IdentifiedXConnectContact]
    public ActionResult BuyConcessions()
    {
      var buyConcessionsInteraction = new BuyCandyInteraction(QueryStringHelper.UserId);
      Task.Run(async () => await buyConcessionsInteraction.ExecuteInteraction()).Wait();
      return Redirect(CinemaConst.Links.SitecoreCinema.Lobby + QueryStringHelper.UserIdQueryString());
    }

    [IdentifiedXConnectContact]
    public ActionResult LobbyOptions()
    {
      var viewModel = new LobbyOptionsViewModel()
      {
        UserId = QueryStringHelper.UserId
      };

      return View(viewModel);
    }

    public ActionResult Register()
    {
      var viewModel = new RegisterViewModel();
      var candidateInfoGenerator = new CandidateInfoGenerator();
      viewModel.CandidateContactInfo = candidateInfoGenerator.GetRandomContactInfo();

      var registerInteraction = new RegisterInteraction(
        viewModel.CandidateContactInfo.FirstName,
        viewModel.CandidateContactInfo.LastName,
        viewModel.CandidateContactInfo.FavoriteMovie,
        viewModel.CandidateContactInfo.Email
        );

      Task.Run(async () => { await registerInteraction.ExecuteInteraction(); }).Wait();

      var idToUse = viewModel.CandidateContactInfo.Email;

      return Redirect(CinemaConst.Links.SitecoreCinema.SelfServiceMachine + QueryStringHelper.UserIdQueryString(idToUse));
    }

    [IdentifiedXConnectContact]
    public ActionResult SelfServiceMachine()
    {
      return View();
    }

    public ActionResult StartJourney()
    {
      return View();
    }

    [IdentifiedXConnectContact]
    public ActionResult WatchMovie()
    {
      var watchMovieInteraction = new WatchMovieInteraction(QueryStringHelper.UserId);
      Task.Run(async () => await watchMovieInteraction.ExecuteInteraction()).Wait();
      return Redirect(CinemaConst.Links.SitecoreCinema.Lobby + QueryStringHelper.UserIdQueryString());
    }

    public ActionResult WhatWeKnowAboutYou()
    {
      Contact trackingContact = Tracker.Current.Session.Contact;
      //var xconnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>(name: "XConnectFacets");

      var knownDataHelper = new KnownDataHelper();

      KnownDataXConnect knownDataXConnect = null;
      KnownDataTracker knownDataTracker = new KnownDataTracker();

      Task.Run(async () => { knownDataXConnect = await knownDataHelper.GetKnownDataByIdentifier(QueryStringHelper.UserId); }).Wait();

      knownDataTracker.IsNew = trackingContact.IsNew;

      if (knownDataXConnect != null)
      {
        knownDataXConnect.UserId = QueryStringHelper.UserId;
        knownDataHelper.AppendCurrentContextData(knownDataXConnect, Sitecore.Context.Database);
      }

      var viewModel = new WhatWeKnowAboutYouViewModel
      {
        KnownDataXConnect = knownDataXConnect,
        KnownDataTracker = knownDataTracker
      };
      return View(viewModel);
    }
  }
}