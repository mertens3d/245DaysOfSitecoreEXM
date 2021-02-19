using Shared.Interfaces;
using Shared.XConnect.Interactions;
using System.Threading.Tasks;

namespace Console.Journey.Steps
{
  public class BuyCandyStep : _journeyStepBase
  {
    public BuyCandyStep(IFeedback feedback, string identifier) : base(feedback, identifier)
    {
    }

    public async Task BuyCandy()
    {
      //   ___       _                      _   _               _  _  ____
      //  |_ _|_ __ | |_ ___ _ __ __ _  ___| |_(_) ___  _ __  _| || ||___ \
      //   | || '_ \| __/ _ \ '__/ _  |/ __| __| |/ _ \| '_ \|_  ..  _|__) |
      //   | || | | | ||  __/ | | (_| | (__| |_| | (_) | | | |_      _/ __/
      //  |___|_| |_|\__\___|_|  \__,_|\___|\__|_|\___/|_| |_| |_||_||_____|

      // You decides to buy some candy and a bottle of water (gotta stay balanced)
      // You use your loyalty card - that's 2 swipes already! The machine sends this interaction to xConnect.

      var arr = new[]  {
                        @"  ____                _       ",
                        @" / ___|__ _ _ __   __| |_   _ ",
                        @"| |   / _` | '_ \ / _` | | | |",
                        @"| |__| (_| | | | | (_| | |_| |",
                        @" \____\__,_|_| |_|\__,_|\__, |",
                        @"                        |___/ ",
                        };
      DrawStepTitle(arr);

      var buyCandyInteraction = new BuyCandyInteraction(Identifier);

      await buyCandyInteraction.ExecuteInteraction();

      DrawTriggerMessage("You swiped your loyalty card.");

      if (buyCandyInteraction.Contact != null)
      {
        System.Console.WriteLine("Why hello again " + buyCandyInteraction.KnownData.details.FirstName + "!");
        System.Console.WriteLine("Candy? You got it.");

        DrawPostInteractionMessage("Enjoy the movie!");
      }
    }
  }
}