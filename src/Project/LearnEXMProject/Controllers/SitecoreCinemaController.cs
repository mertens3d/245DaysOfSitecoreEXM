using LearnEXMProject.Models.SitecoreCinema;
using System;
using System.Web.Mvc;

namespace LearnEXMProject.Controllers
{
  public class SitecoreCinemaController : Controller
  {
    public ActionResult StartJourney()
    {
      return View();
    }

    public ActionResult Register()
    {
      var viewModel = new RegisterViewModel();
      var nameGenerator = new CandidateNames();
      viewModel.CandidateNames = nameGenerator.GetSomeNames();
      return View(viewModel);
    }

    public ActionResult SelfServiceMachine()
    {
      return View();
    }

    public ActionResult WhatWeKnowAboutYou()
    {
      return View();
    }
  }
}