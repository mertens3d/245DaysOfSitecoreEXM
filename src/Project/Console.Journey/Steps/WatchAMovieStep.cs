using Shared.Interfaces;
using Shared.XConnect.Interactions;
using System.Threading.Tasks;

namespace Console.Journey.Interactions
{
  public class WatchAMovieStep : _journeyStepBase
  {
    //   ___       _                      _   _               _  _  _____
    //  |_ _|_ __ | |_ ___ _ __ __ _  ___| |_(_) ___  _ __  _| || ||___ /
    //   | || '_ \| __/ _ \ '__/ _  |/ __| __| |/ _ \| '_ \|_  ..  _||_ \
    //   | || | | | ||  __/ | | (_| | (__| |_| | (_) | | | |_      _|__) |
    //  |___|_| |_|\__\___|_|  \__,_|\___|\__|_|\___/|_| |_| |_||_||____/

    // Finally, you scan your ticket and head in - your loyalty card details
    // are embedded in the barcode of your ticket. At this point, the system takes payment.
    public WatchAMovieStep(IFeedback feedback, string identifier) : base(feedback, identifier) { }

    public async Task WatchAMovieAsync()
    {
      var watchMovieInteration = new WatchMovieInteraction(Feedback, Identifier);

      var arr = new[]
                {
                        @" __  __            _      ",
                        @"|  \/  | _____   _(_) ___ ",
                        @"| |\/| |/ _ \ \ / / |/ _ \",
                        @"| |  | | (_) \ V /| |  __/",
                        @"|_|  |_|\___/ \_/ |_|\___|",
                        };

      if (watchMovieInteration.Initializer.InitSuccess)
      {
        await watchMovieInteration.ExecuteInteraction();

        System.Console.WriteLine("Enjoy your movie, " + watchMovieInteration.KnownData.details.FirstName + "!");
        System.Console.WriteLine("Since you're a loyalty card holder, we'll take payment for your ticket now.");
      }

      // Initialize a client using the validate configuration

      if (watchMovieInteration.Contact != null)
      {
        DrawTriggerMessage("You scanned your ticket - the bar code has your loyalty card information embedded in it.");
      }

      System.Console.ReadKey();
    }
  }
}