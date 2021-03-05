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
    public string UserId { get; private set; } = string.Empty;

    public SitecoreCinemaController()
    {
    }

    public ActionResult StartJourney()
    {
      return View();
    }

    public ActionResult LobbyOptions()
    {
      // if userid provided, then show options
      // else redirect back to root
      IdentifyUser();
      var viewModel = new LobbyOptionsViewModel()
      {
        UserId = UserId
      };

      return View(viewModel);
    }

    public ActionResult BuyConcessions()
    {
      IdentifyUser();
      var buyConcessionsInteraction = new BuyCandyInteraction(UserId);
      Task.Run(async () => await buyConcessionsInteraction.ExecuteInteraction()).Wait();
      return Redirect(CinemaConst.Links.SitecoreCinema.Lobby + UserIdQueryString());
    }

    public ActionResult WatchMovie()
    {
      IdentifyUser();
      var watchMovieInteraction = new WatchMovieInteraction(UserId);
      Task.Run(async () => await watchMovieInteraction.ExecuteInteraction()).Wait();
      return Redirect(CinemaConst.Links.SitecoreCinema.Lobby + UserIdQueryString());
    }

    public ActionResult Register()
    {
      //check if already registered
      // if not, then register and then redirect
      // if so, then just redirect
      IdentifyUser();
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

      UserId = viewModel.CandidateContactInfo.Email;

      return Redirect(CinemaConst.Links.SitecoreCinema.SelfServiceMachine + UserIdQueryString());
    }

    private string UserIdQueryString()
    {
      return $"?&{Shared.Const.QueryString.UserIdKey}={UserId}";
    }

    public ActionResult SelfServiceMachine()
    {
      return View();
    }

    private void IdentifyUser()
    {
      if (Request != null)
      {
        UserId = Request[Shared.Const.QueryString.UserIdKey];
      }
      else
      {
        Sitecore.Diagnostics.Log.Error("Request was null", this);
      }

      if (!string.IsNullOrEmpty(UserId))
      {
        Tracker.Current.Session.IdentifyAs(Shared.Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, UserId);
      }
    }

    public ActionResult WhatWeKnowAboutYou()
    {
      IdentifyUser();
      Contact trackingContact = Tracker.Current.Session.Contact;

      //var xconnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>(name: "XConnectFacets");

      var knownDataHelper = new KnownDataHelper();

      KnownDataXConnect knownDataXConnect = null;
      KnownDataTracker knownDataTracker = new KnownDataTracker();

      Task.Run(async () => { knownDataXConnect = await knownDataHelper.GetKnownDataByIdentifier(UserId); }).Wait();

      knownDataTracker.IsNew = trackingContact.IsNew;

      if (knownDataXConnect != null)
      {
        knownDataXConnect.UserId = UserId;
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