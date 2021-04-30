using LearnEXM.Feature.MockContactGenerator.Helpers;
using LearnEXM.Feature.SitecoreCinema.Helpers;
using LearnEXM.Feature.SitecoreCinema.Models;
using LearnEXM.Feature.SitecoreCinema.Models.ViewModels;
using LearnEXM.Foundation.CollectionModel.Builder.Interactions;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Project.SitecoreCinema.Model;
using LearnEXM.Project.SitecoreCinema.Model.ViewModels;
using LearnEXM.Project.SitecoreCinema.MVCHelpers;
using Sitecore.Analytics;
using Sitecore.Analytics.Model.Entities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LearnEXM.Project.SitecoreCinema.Controllers
{
  public class SitecoreCinemaController : Controller
  {
    public SitecoreCinemaController()
    {
    }

    [IdentifiedXConnectContact]
    public ActionResult BuyConcessions()
    {
      var buyConcessionsInteraction = new BuyCandyInteraction(Tracker.Current.Contact);

      buyConcessionsInteraction.ExecuteInteraction();
      return Redirect(Feature.SitecoreCinema.ProjectConst.Links.SitecoreCinema.Lobby.LobbyLanding);
    }

    private ContactIdentifier GetSitecoreCinemaContactIdentifier()
    {
      //I think i am not identifying as correctly in the auto so the tracker doesn't know i'm known

      ContactIdentifier toReturn = Tracker.Current.Contact.Identifiers.FirstOrDefault(x => x.Source == Foundation.CollectionModel.Builder.CollectionConst.XConnect.ContactIdentifiers.Sources.SitecoreCinema);
      return toReturn;
    }

    [IdentifiedXConnectContact]
    public ActionResult BuyTicket(Guid movieid)
    {
      Guid cinemaId = Guid.Parse("{A83BEBA7-0752-4A11-8F3A-95F84B53A0D4}"); //todo build in cinemas
      var movieItemProxy = new MovieShowTimeProxy(movieid);

      var movieTicket = new MovieTicket()
      {
        CinimaId = cinemaId,
        MovieId = movieItemProxy.MovieShowTimeId.Guid,
        MovieName = movieItemProxy.MovieName
      };

      var buyTicketInteraction = new SelfServiceMachineInteraction(Tracker.Current.Contact, movieTicket);
      buyTicketInteraction.ExecuteInteraction();
      return Redirect(Feature.SitecoreCinema.ProjectConst.Links.SitecoreCinema.Lobby.LobbyLanding);
    }

    [IdentifiedXConnectContact]
    public ActionResult Lobby()
    {
      var viewModel = new LobbyViewModel();

      var concessionHelper = new ConcessionHelper();
      viewModel.ConcessionCategories = concessionHelper.GetConcessions();

      return View(ProjectConst.ControllerViews.Lobby, viewModel);
    }

    public ActionResult RegisterViaAutoRandom()
    {
      var candidateInfoGenerator = new MockContactGeneratorHelper();// MockContactGenerator();

      var CandidateContactInfo = candidateInfoGenerator.GenerateContact();

      try
      {
        Tracker.Current.Session.IdentifyAs(Foundation.CollectionModel.Builder.CollectionConst.XConnect.ContactIdentifiers.Sources.SitecoreCinema, CandidateContactInfo.EmailAddress);

        var registerInteraction = new UpdateContactInfoInteraction(CandidateContactInfo, Tracker.Current.Contact);

        registerInteraction.ExecuteInteraction();
      }
      catch (System.Exception ex)
      {
        Sitecore.Diagnostics.Log.Error(ex.Message, this);
      }

      return Redirect(Feature.SitecoreCinema.ProjectConst.Links.SitecoreCinema.SelfServiceMachine);
    }

    [IdentifiedXConnectContact]
    public ActionResult SelfServiceMachine()
    {
      ActionResult actionResult;

      try
      {
        var viewModel = new SelfServiceMachineViewModel();
        //var movieTicketHelper = new MovieTicketHelper();
        //viewModel.ShowTimes = movieTicketHelper.AvailableMovies();
        viewModel.MovieShowTimesProxy = new MovieShowTimesProxy(LearnEXM.Feature.SitecoreCinema.ProjectConst.Items.Content.MovieShowTimesFolderItemID);
       actionResult = View(ProjectConst.ControllerViews.SelfServiceMachine, viewModel);
      }
      catch (Exception ex)
      {
        Sitecore.Diagnostics.Log.Error(ProjectConst.Logging.Prefix + "SelfServiceMachine ", ex, this);
        actionResult =new  RedirectResult(ProjectConst.Pages.ErrorPage);
      }

      return actionResult;
    }

    public ActionResult StartJourney()
    {
      if (Tracker.Current.Session.Contact.IdentificationLevel == Sitecore.Analytics.Model.ContactIdentificationLevel.Known)
      {
        return Redirect(Feature.SitecoreCinema.ProjectConst.Links.SitecoreCinema.SelfServiceMachine);
      }
      else
      {
        return View(ProjectConst.ControllerViews.StartJourney);
      }
    }

    [IdentifiedXConnectContact]
    public ActionResult WatchMovie()
    {
      var watchMovieInteraction = new WatchMovieInteraction(Tracker.Current.Contact);
      watchMovieInteraction.ExecuteInteraction();

      var viewModel = new WatchMovieViewModel(Tracker.Current.Contact);

      return View(ProjectConst.ControllerViews.WatchMovie, viewModel);
    }
  }
}