﻿using LearnEXM.Feature.WhatWeKnowAboutYou.Helpers;
using LearnEXM.Feature.WhatWeKnowAboutYou.Models;
using LearnEXM.Foundation.CollectionModel.Builder;
using LearnEXM.Foundation.CollectionModel.Builder.XConnect.Interactions;
using LearnEXMProject.Models;
using LearnEXMProject.Models.SitecoreCinema;
using Sitecore.Analytics;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Tracking;
using System.Linq;
using System.Web.Mvc;

namespace LearnEXMProject.Controllers
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
      return Redirect(WebConst.Links.SitecoreCinema.Lobby);
    }

    private ContactIdentifier GetSitecoreCinemaContactIdentifier()
    {
      //I think i am not identifying as correctly in the auto so the tracker doesn't know i'm known

      ContactIdentifier toReturn = Tracker.Current.Contact.Identifiers.FirstOrDefault(x => x.Source == LearnEXM.Foundation.CollectionModel.Builder.CollectionConst.XConnect.ContactIdentifiers.Sources.SitecoreCinema);
      return toReturn;
    }

    [IdentifiedXConnectContact]
    public ActionResult BuyTicket()
    {
      var buyTicketInteraction = new SelfServiceMachineInteraction(Tracker.Current.Contact);
      buyTicketInteraction.ExecuteInteraction();
      return Redirect(WebConst.Links.SitecoreCinema.Lobby);
    }

    [IdentifiedXConnectContact]
    public ActionResult LobbyOptions()
    {
      var viewModel = new LobbyOptionsViewModel();
      return View(viewModel);
    }

    public ActionResult RegisterViaAutoRandom()
    {
      var candidateInfoGenerator = new CandidateInfoGenerator();
      var CandidateContactInfo = candidateInfoGenerator.GetRandomContactInfo();

      try
      {
        Tracker.Current.Session.IdentifyAs(LearnEXM.Foundation.CollectionModel.Builder.CollectionConst.XConnect.ContactIdentifiers.Sources.SitecoreCinema, CandidateContactInfo.Email);

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

      return Redirect(WebConst.Links.SitecoreCinema.SelfServiceMachine);
    }

    [IdentifiedXConnectContact]
    public ActionResult SelfServiceMachine()
    {
      var viewModel = new SelfServiceMachineViewModel(Tracker.Current.Contact);

      return View(viewModel);
    }

    public ActionResult StartJourney()
    {
      if (Tracker.Current.Session.Contact.IdentificationLevel == Sitecore.Analytics.Model.ContactIdentificationLevel.Known)
      {
        return Redirect(WebConst.Links.SitecoreCinema.SelfServiceMachine);
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
      return Redirect(WebConst.Links.SitecoreCinema.Lobby);
    }

    public ActionResult WhatWeKnowAboutYou()
    {
      Contact trackingContact = Tracker.Current.Contact;

      var knownDataHelper = new KnownDataHelper();

      KnownData knownDataXConnect = null;

      //Task.Run(async () =>
      //{
      //  knownDataXConnect = await knownDataHelper.GetKnownDataByIdentifierViaXConnect(QueryStringHelper.UserId);
      //}
      //).Wait();

      KnownData knownDataViaTracker = null;// knownDataHelper.GetKnownDataViaTracker(trackingContact);

      //Tracker.Current.Contact // <--- Use this
      // use other Contact outside of a web page.

      //knownDataTracker.IsNew = trackingContact.IsNew;

      if (knownDataXConnect != null)
      {
        knownDataHelper.AppendCurrentContextData(knownDataXConnect, Sitecore.Context.Database);
      }

      var viewModel = new WhatWeKnowAboutYouViewModel
      {
        KnownDataXConnect = knownDataViaTracker,
        KnownDataTracker = null
      };
      return View(viewModel);
    }
  }
}