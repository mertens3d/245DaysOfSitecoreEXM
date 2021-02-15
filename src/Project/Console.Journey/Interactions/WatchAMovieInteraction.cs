using Console.Journey.JourneySteps;
using Shared;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using SitecoreCinema.Model.Collection;
using System;
using System.Threading.Tasks;

namespace Console.Journey.Interactions
{

  public partial class WatchAMovieInteraction : _interactionBase
  {
    //   ___       _                      _   _               _  _  _____
    //  |_ _|_ __ | |_ ___ _ __ __ _  ___| |_(_) ___  _ __  _| || ||___ /
    //   | || '_ \| __/ _ \ '__/ _  |/ __| __| |/ _ \| '_ \|_  ..  _||_ \
    //   | || | | | ||  __/ | | (_| | (__| |_| | (_) | | | |_      _|__) |
    //  |___|_| |_|\__\___|_|  \__,_|\___|\__|_|\___/|_| |_| |_||_||____/

    // Finally, you scan your ticket and head in - your loyalty card details
    // are embedded in the barcode of your ticket. At this point, the system takes payment.
    public WatchAMovieInteraction(string identifier) : base(identifier) { }

    public async Task WatchAMovieAsync()
    {
      var cfgGenerator = new CFGGenerator();

      var cfg = cfgGenerator.GetCFG(SitecoreCinemaModel.Model);

      var arr = new[]
                {
                        @" __  __            _      ",
                        @"|  \/  | _____   _(_) ___ ",
                        @"| |\/| |/ _ \ \ / / |/ _ \",
                        @"| |  | | (_) \ V /| |  __/",
                        @"|_|  |_|\___/ \_/ |_|\___|",
                        };

      var initializer = new Initializer();
      await initializer.InitCFGAsync(cfg, arr);

      // Initialize a client using the validate configuration

      using (Client = new XConnectClient(cfg))
      {
        try
        {
          DrawTriggerMessage("You scanned your ticket - the bar code has your loyalty card information embedded in it.");
          await PopulateContactDataAsync();

          if (Contact != null)
          {
            System.Console.WriteLine("Enjoy your movie, " + PersonalInfo.FirstName + "!");
            System.Console.WriteLine("Since you're a loyalty card holder, we'll take payment for your ticket now.");

            var interaction = new Interaction(Contact, InteractionInitiator.Contact, Shared.Const.XConnect.Channels.WatchedMovie, "");

            interaction.Events.Add(new WatchMovie(DateTime.UtcNow, "Dkk", 100m)
            {
              EIDR = Shared.Const.XConnect.MovieEIDR.DieHard
            }
              );

            Client.SetFacet(interaction, SitecoreCinema.Model.Collection.CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = Shared.Const.XConnect.CinemaId.Theater22 });

            Client.AddInteraction(interaction);
            await Client.SubmitAsync();

            var reporter = new ReportContactData();
            await reporter.ReportAsync(Client, Contact);

          }
        }
        catch (XdbModelConflictException ce)

        {
        }
      }


      System.Console.ReadKey();

    }
  }
}