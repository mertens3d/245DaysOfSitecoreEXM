using Console.Journey.Interactions;
using Console.Journey.Steps;
using Shared.Interactions;
using Shared.XConnect.Interactions;
using System.Threading.Tasks;

namespace Console.Journey
{
  internal class Program
  {
    private static async Task AllStepsAsync()
    {
      // Welcome to...
      //   _____ _ __                            _______
      //  / ___/(_) /____  _________  ________  / ____(_)___  ___  ____ ___  ____ _
      //  \__ \/ / __/ _ \/ ___/ __ \/ ___/ _ \/ /   / / __ \/ _ \/ __ `__ \/ __ `/
      // ___/ / / /_/  __/ /__/ /_/ / /  /  __/ /___/ / / / /  __/ / / / / / /_/ /
      // ____/_/\__/\___/\___/\____/_/   \___/\____/_/_/ /_/\___/_/ /_/ /_/\__,_/

      var feedback = new ConsoleFeedback();

      feedback.WriteLine("What is your first name?");

      var firstname = feedback.ReadLine();

      feedback.WriteLine("What is your last name?");

      var lastname = feedback.ReadLine();

      feedback.WriteLine("Favorite movie?");

      var favoriteMovie = feedback.ReadLine();

      var registerStep = new RegisterInteraction(firstname, lastname, favoriteMovie);
      Task.Run(async () => { await registerStep.ExecuteInteraction(); })
        .Wait();

      var selfServiceMachineStep = new SelfServiceStep(feedback, registerStep. Identifier);

      Task.Run(async () => { await selfServiceMachineStep.ExecuteStep(); }).Wait();

      var buyCandyStep = new BuyCandyStep(feedback, registerStep.Identifier);
      Task.Run(async () => { await buyCandyStep.BuyCandy(); }).Wait();

      var watchMovieInteraction = new Interactions.WatchAMovieStep(feedback, registerStep.Identifier);
      Task.Run(async () => { await watchMovieInteraction.WatchAMovieAsync(); }).Wait();

      var reporter = new ReportContactDataStep(feedback, registerStep.Identifier);
      Task.Run(async () => { await reporter.ReportAsync(); }).Wait();
    }

    private static void Main(string[] args)
    {
      Task.Run(async () => { await AllStepsAsync(); });
    }
  }
}