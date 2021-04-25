using LearnEXM.Feature.MockContactGenerator;
using LearnEXM.Foundation.CollectionModel.Builder.Interactions;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Project.SitecoreCinema.Controllers.Helpers;
using LearnEXM.Project.SitecoreCinema.Model;
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
      return Redirect(ProjConst.Links.SitecoreCinema.Lobby.LobbyLanding);
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
      var movieDataProxy = new MovieItemProxy(movieid);

      var movieTicket = new MovieTicket()
      {
        CinimaId = cinemaId,
        MovieId = movieDataProxy.MovieItem.ID.Guid,
        MovieName = movieDataProxy.MovieName
      };

      var buyTicketInteraction = new SelfServiceMachineInteraction(Tracker.Current.Contact, movieTicket);
      buyTicketInteraction.ExecuteInteraction();
      return Redirect(ProjConst.Links.SitecoreCinema.Lobby.LobbyLanding);
    }

    [IdentifiedXConnectContact]
    public ActionResult LobbyOptions()
    {
      var viewModel = new LobbyOptionsViewModel();
      return View(viewModel);
    }

    public ActionResult RegisterViaAutoRandom()
    {
      var candidateInfoGenerator = new MockContactGenerator();
      var CandidateContactInfo = candidateInfoGenerator.GetRandomContactInfo();

      try
      {
        Tracker.Current.Session.IdentifyAs(Foundation.CollectionModel.Builder.CollectionConst.XConnect.ContactIdentifiers.Sources.SitecoreCinema, CandidateContactInfo.Email);

        var registerInteraction = new UpdateContactInfoInteraction(
          CandidateContactInfo.FirstName,
          CandidateContactInfo.LastName,
          CandidateContactInfo.FavoriteMovie,
          CandidateContactInfo.Email,
          CandidateContactInfo.Email,
          Tracker.Current.Contact
          );

        registerInteraction.ExecuteInteraction();

      }
      catch (System.Exception ex)
      {
        Sitecore.Diagnostics.Log.Error(ex.Message, this);
      }


        return Redirect(ProjConst.Links.SitecoreCinema.SelfServiceMachine);
      

    }

    [IdentifiedXConnectContact]
    public ActionResult SelfServiceMachine()
    {
      var viewModel = new SelfServiceMachineViewModel(Tracker.Current.Contact);
      var movieTicketHelper = new MovieTicketHelper();
      viewModel.ShowTimes = movieTicketHelper.AvailableMovies();
      return View(viewModel);
    }

    public ActionResult StartJourney()
    {
      if (Tracker.Current.Session.Contact.IdentificationLevel == Sitecore.Analytics.Model.ContactIdentificationLevel.Known)
      {
        return Redirect(ProjConst.Links.SitecoreCinema.SelfServiceMachine);
      }
      else
      {
        return View();
      }
    }

    [IdentifiedXConnectContact]
    public ActionResult WatchMovie()
    {
      var watchMovieInteraction = new WatchMovieInteraction(Tracker.Current.Contact);
      watchMovieInteraction.ExecuteInteraction();


      var viewModel = new WatchMovieViewModel(Tracker.Current.Contact);

      return View(viewModel);
      //return Redirect(WebConst.Links.SitecoreCinema.Lobby.LobbyLanding);
    }
  }
}