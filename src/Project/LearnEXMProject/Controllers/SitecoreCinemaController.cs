using LearnEXMProject.Models.SitecoreCinema;
using Shared.Models;
using Shared.Models.SitecoreCinema;
using Shared.Models.SitecoreCinema.Collection;
using Shared.XConnect.Interactions;
using Sitecore.Analytics;
using Sitecore.Analytics.Tracking;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect.Collection.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LearnEXMProject.Controllers
{
  public class SitecoreCinemaController : Controller
  {
    public WebpageFeedback Feedback { get; }
    public string UserId { get; private set; } = string.Empty;

    public SitecoreCinemaController()
    {
      Feedback = new WebpageFeedback();
    }

    public ActionResult StartJourney()
    {
      return View();
    }

    public void IdentifyUser(string userName)
    {
      var toReturn = string.Empty;

      if (userName.ToLower() == "extranet\\anonymous")
      { }
      else
      {
        string identificationSource = "website";
        if (Tracker.Current != null && Tracker.Current.IsActive && Tracker.Current.Session != null)
        {
          Tracker.Current.Session.IdentifyAs(identificationSource, userName);
        }
      }
    }

    public ActionResult Register()
    {
      IdentifyUser();

      var viewModel = new RegisterViewModel();
      var candidateInfoGenerator = new CandidateInfoGenerator();
      viewModel.CandidateContactInfo = candidateInfoGenerator.GetRandomContactInfo();

      

      var registerInteraction = new RegisterInteraction(
        viewModel.CandidateContactInfo.FirstName,
        viewModel.CandidateContactInfo.LastName,
        viewModel.CandidateContactInfo.FavoriteMovie,
        UserId
        ) ;

      Task.Run(async () => { await registerInteraction.ExecuteInteraction(); }).Wait();

      return View(viewModel);
    }

    public ActionResult SelfServiceMachine()
    {
      return View();
    }

    private void IdentifyUser()
    {
      User toReturn = new User();
      toReturn.UserId = Request["userid"];
      UserId = Request["userId"];
      if (!string.IsNullOrEmpty(toReturn.UserId))
      {
        Tracker.Current.Session.IdentifyAs(Shared.Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, toReturn.UserId);
      }
    }

    public ActionResult WhatWeKnowAboutYou()
    {
      Contact contact = Tracker.Current.Session.Contact;

      IdentifyUser();

      var xconnectFacets = Tracker.Current.Contact.GetFacet<IXConnectFacets>(name: "XConnectFacets");

      PersonalInformation personalInformation = null;
      CinemaVisitorInfo cinemaVisitorInfo = null;
      //CinemaInfo cinemaInfo = null;

      if (xconnectFacets.Facets != null)
      {
        personalInformation = xconnectFacets.Facets[PersonalInformation.DefaultFacetKey] as PersonalInformation;
        cinemaVisitorInfo = xconnectFacets.Facets[CinemaVisitorInfo.DefaultFacetKey] as CinemaVisitorInfo;
        //cinemaInfo = xconnectFacets.Facets[CinemaInfo.DefaultFacetKey] as CinemaInfo;
      }
      else
      {
        Sitecore.Diagnostics.Log.Debug("No facets", this);
      }

      //var dataReporter = new DataReporter();
      KnownData knownData = new KnownData();
      knownData.details = personalInformation;
      knownData.movie = cinemaVisitorInfo;
      knownData.Id = contact.ContactId;
      //Task.Run(async () =>
      //{
      //  knownData = await dataReporter.GetKnownDataByIdentifier(contact.Identifiers.ToList().FirstOrDefault().Identifier);
      //}
      //).Wait();

      
      var viewModel = new WhatWeKnowAboutYouViewModel
      {
        KnownData = knownData,
        
      };
      return View(knownData);
    }
  }
}