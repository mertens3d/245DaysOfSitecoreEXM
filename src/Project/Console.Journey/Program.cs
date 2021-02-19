using Console.Journey.Steps;
using System.Threading.Tasks;

namespace Console.Journey
{
  internal class Program
  {
    private static void ExecuteAllSteps()
    {
      // Welcome to...
      //   _____ _ __                            _______
      //  / ___/(_) /____  _________  ________  / ____(_)___  ___  ____ ___  ____ _
      //  \__ \/ / __/ _ \/ ___/ __ \/ ___/ _ \/ /   / / __ \/ _ \/ __ `__ \/ __ `/
      // ___/ / / /_/  __/ /__/ /_/ / /  /  __/ /___/ / / / /  __/ / / / / / /_/ /
      // ____/_/\__/\___/\___/\____/_/   \___/\____/_/_/ /_/\___/_/ /_/ /_/\__,_/

      var feedback = new ConsoleFeedback();
      string Identifier = string.Empty;

      var registerStep = new RegisterStep(feedback);
      Task.Run(async () => { Identifier = await registerStep.Register(); }).Wait();

      var selfServiceMachineStep = new SelfServiceStep(feedback, Identifier);
      Task.Run(async () => { await selfServiceMachineStep.ExecuteStep(); }).Wait();

      var buyCandyStep = new BuyCandyStep(feedback, Identifier);
      Task.Run(async () => { await buyCandyStep.BuyCandy(); }).Wait();

      var watchMovieStep = new WatchAMovieStep(feedback, Identifier);
      Task.Run(async () => { await watchMovieStep.WatchAMovieAsync(); }).Wait();

      var reporter = new ReportContactDataStep(feedback, Identifier);
      Task.Run(async () => { await reporter.ReportAsync(); }).Wait();

      feedback.ReadKey();
    }

    private static void Main(string[] args)
    {
      ExecuteAllSteps();
    }
  }
}