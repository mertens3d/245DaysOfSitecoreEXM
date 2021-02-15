using Console.Journey.Interactions;
using Console.Journey.JourneySteps;
using System.Threading.Tasks;
using static Console.Journey.Interactions.WatchAMovieInteraction;

namespace Console.Journey
{
  internal class Program
  {
    public static string Identifier { get; set; }

    private static void Main(string[] args)
    {
      // Welcome to...
      //   _____ _ __                            _______
      //  / ___/(_) /____  _________  ________  / ____(_)___  ___  ____ ___  ____ _
      //  \__ \/ / __/ _ \/ ___/ __ \/ ___/ _ \/ /   / / __ \/ _ \/ __ `__ \/ __ `/
      // ___/ / / /_/  __/ /__/ /_/ / /  /  __/ /___/ / / / /  __/ / / / / / /_/ /
      // ____/_/\__/\___/\___/\____/_/   \___/\____/_/_/ /_/\___/_/ /_/ /_/\__,_/

      var registerStep = new RegisterStep();

      Task.Run(async () => { Identifier = await registerStep.Register(); })
        .Wait();

      var selfServiceMachineStep = new SelfServiceMachineInteraction(Identifier);
      Task.Run(async () => { await selfServiceMachineStep.SelfServiceMachine(); }).Wait();

      var buyCandyStep = new BuyCandyInteraction(Identifier);
      Task.Run(async () => { await buyCandyStep.BuyCandy(); }).Wait();


      var watchMovieInteraction = new WatchAMovieInteraction(Identifier);
      Task.Run(async () => { await watchMovieInteraction.WatchAMovieAsync(); }).Wait();


      
    }
  }
}