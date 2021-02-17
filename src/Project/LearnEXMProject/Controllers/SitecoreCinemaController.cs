using LearnEXMProject.Models.SitecoreCinema;
using Shared.XConnect.Interactions;
using Sitecore.Analytics;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LearnEXMProject.Controllers
{
  public class SitecoreCinemaController : Controller
  {
    public WebpageFeedback Feedback { get; }

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
      var viewModel = new RegisterViewModel();
      var candidateInfoGenerator = new CandidateInfoGenerator();
      viewModel.CandidateContactInfo = candidateInfoGenerator.GetRandomContactInfo();

      var registerStep = new RegisterInteraction(viewModel.CandidateContactInfo.FirstName, viewModel.CandidateContactInfo.LastName, viewModel.CandidateContactInfo.FavoriteMovie);

      Task.Run(async () => { await registerStep.ExecuteInteraction(); }).Wait();

      return View(viewModel);
    }

    public ActionResult SelfServiceMachine()
    {
      return View();
    }

    public async Task<ActionResult> WhatWeKnowAboutYouAsync()
    {
      string identifier = "todo";
      var contactData = new ReportKnownData(identifier);
      var viewModel = new WhatWeKnowAboutYouViewModel();
      viewModel.KnownData = contactData.KnownData;
      return View();
    }
  }
}