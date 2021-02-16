using Console.Journey.JourneySteps;
using Shared;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using SitecoreCinema.Model.Collection;
using System;
using System.Threading.Tasks;

namespace Console.Journey.Interactions
{
  public class SelfServiceMachineInteraction : _interactionBase
  {
    public SelfServiceMachineInteraction(string identifier) : base(identifier)
    {
    }

    public async Task SelfServiceMachine()
    {
      //   ___       _                      _   _               _  _   _
      //  |_ _|_ __ | |_ ___ _ __ __ _  ___| |_(_) ___  _ __  _| || |_/ |
      //   | || '_ \| __/ _ \ '__/ _  |/ __| __| |/ _ \| '_ \|_  ..  _| |
      //   | || | | | ||  __/ | | (_| | (__| |_| | (_) | | | |_      _| |
      //  |___|_| |_|\__\___|_|  \__,_|\___|\__|_|\___/|_| |_| |_||_| |_|

      // You cycle to the nearest Sitecore Cinema (which has great bicycle storage facilities, by the way)
      // and use a self service machine to buy a ticket. You swipes your loyalty card - the machine
      // immediately sends this interaction to xConnect. Because you're a loyalty card member
      // you don't even pay at this point!

      var cfgGenerator = new CFGGenerator();

      var cfg = cfgGenerator.GetCFG(SitecoreCinemaModel.Model);

      var arr = new[]
                {
                        @" _____ _      _        _   ",
                        @"|_   _(_) ___| | _____| |_ ",
                        @"  | | | |/ __| |/ / _ \ __|",
                        @"  | | | | (__|   <  __/ |_ ",
                        @"  |_| |_|\___|_|\_\___|\__|",
                        };

      var initializer = new Initializer();
      await initializer.InitCFGAsync(cfg, arr);

      // Initialize a client using the validate configuration

      using (Client = new XConnectClient(cfg))
      {
        try
        {
          DrawTriggerMessage("You swiped your loyalty card.");

          await PopulateContactDataAsync();

          if (Contact != null)
          {
            System.Console.WriteLine("Why hello there " + PersonalInfo.FirstName + " " + PersonalInfo.LastName + ", whose favorite film is..." + CinemaInfo.FavoriteMovie + ". Wow, really? Ok, to each their own I guess.");

            var interaction = new Interaction(Contact, InteractionInitiator.Contact, Const.XConnect.Channels.BoughtTicket, ""); // Guid should be from a channel in sitecore

            Client.SetFacet(interaction, SitecoreCinema.Model.Collection.CinemaInfo.DefaultFacetKey, new CinemaInfo() { CinimaId = Const.XConnect.CinemaId.Theater22 });

            interaction.Events.Add(new UseSelfService(DateTime.UtcNow));

            Client.AddInteraction(interaction);

            await Client.SubmitAsync();

            DrawPostInteractionMessage(new string[]{
            "Here's your ticket - we'll charge you when you use it, in case you have some sort of emergency between here and the movie",
            "It's just one of those courtesies we offer loyalty card member! Now go buy some candy." }
            );
          }
        }
        catch (Exception ex)
        {
          // deal with exception
        }
      }
    }
  }
}