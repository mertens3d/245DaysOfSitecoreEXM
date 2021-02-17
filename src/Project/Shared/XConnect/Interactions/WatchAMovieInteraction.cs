//using Shared.Interfaces;
//using Sitecore.XConnect;
//using Sitecore.XConnect.Client;
//using SitecoreCinema.Model.Collection;
//using System;
//using System.Threading.Tasks;

//namespace Shared.Interactions
//{
//  public class WatchAMovieInteraction : _interactionBase
//  {
    
//    public WatchAMovieInteraction(IFeedback feedback, string identifier) : base(feedback, identifier) { }

//    public async Task WatchAMovieAsync()
//    {
//      var cfgGenerator = new CFGGenerator();

//      var cfg = cfgGenerator.GetCFG(SitecoreCinemaModel.Model);


//      var initializer = new Initializer(Feedback);

//      // Initialize a client using the validate configuration

//      using (Client = new XConnectClient(cfg))
//      {
//        try
//        {
//          DrawTriggerMessage("You scanned your ticket - the bar code has your loyalty card information embedded in it.");
//          await PopulateContactDataAsync();

//          if (Contact != null)
//          {
//            Feedback.WriteLine("Enjoy your movie, " + PersonalInfo.FirstName + "!");
//            Feedback.WriteLine("Since you're a loyalty card holder, we'll take payment for your ticket now.");

//            var interaction = new Interaction(Contact, InteractionInitiator.Contact, Const.XConnect.Channels.WatchedMovie, "");

//            interaction.Events.Add(new WatchMovie(DateTime.UtcNow, "Dkk", 100m)
//            {
//              EIDR = Const.XConnect.MovieEIDR.DieHard
//            }
//              );

//            Client.SetFacet(interaction, SitecoreCinema.Model.Collection.CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = Const.XConnect.CinemaId.Theater22 });

//            Client.AddInteraction(interaction);
//            await Client.SubmitAsync();

//            var reporter = new ReportContactData(Feedback);
//            await reporter.ReportAsync(Client, Contact);

//          }
//        }
//        catch (XdbModelConflictException ce)

//        {
//        }
//      }


//      Feedback.ReadKey();

//    }
//  }
//}