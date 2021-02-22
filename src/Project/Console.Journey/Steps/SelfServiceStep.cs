using Shared.Interfaces;
using Shared.Models;
using Shared.XConnect;
using Shared.XConnect.Interactions;
using System.Linq;
using System.Threading.Tasks;

namespace Console.Journey.Steps
{
  public class SelfServiceStep : _journeyStepBase
  {
    public SelfServiceStep(IFeedback feedback, string identifier) : base(feedback, identifier)
    {
    }

    public async Task ExecuteStep()
    {
      var arr = new[]
               {
                        @" _____ _      _        _   ",
                        @"|_   _(_) ___| | _____| |_ ",
                        @"  | | | |/ __| |/ / _ \ __|",
                        @"  | | | | (__|   <  __/ |_ ",
                        @"  |_| |_|\___|_|\_\___|\__|",
                        };

      DrawStepTitle(arr);
      DrawTriggerMessage("You swiped your loyalty card.");

      var selfServiceMachineInteraction = new SelfServiceMachineInteraction(Identifier);

      await selfServiceMachineInteraction.ExecuteInteraction();

      var reporter = new DataReporter();
      KnownData knownData = await reporter.GetKnownDataByIdentifier(Identifier);

      if (!selfServiceMachineInteraction.Errors.Any() && knownData?.details != null
        && knownData.movie != null)
      {
        Feedback.WriteLine("Why hello there " +
          knownData.details.FirstName + " "
          + knownData.details.LastName
          + ", whose favorite film is..." + knownData.movie.FavoriteMovie
          + ". Wow, really? Ok, to each their own I guess.");

        DrawPostInteractionMessage(new string[]{
            "Here's your ticket - we'll charge you when you use it, in case you have some sort of emergency between here and the movie",
            "It's just one of those courtesies we offer loyalty card member! Now go buy some candy." }
         );
      }
    }
  }
}