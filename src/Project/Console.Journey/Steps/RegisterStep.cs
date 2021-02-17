using Console.Journey.Interactions;
using Shared.Interfaces;
using Shared.XConnect.Interactions;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Threading.Tasks;

namespace Console.Journey.JourneySteps
{
  public class RegisterStep : _journeyStepBase
  {
    public RegisterStep(IFeedback feedback) : base(feedback, "")
    {
    }

    public async Task<string> Register()
    {
      //   ____            _     _
      //  |  _ \ ___  __ _(_)___| |_ ___ _ __
      //  | |_) / _ \/ _` | / __| __/ _ \ '__|
      //  |  _ <  __/ (_| | \__ \ ||  __/ |
      //  |_| \_\___|\__, |_|___/\__\___|_|
      //             |___/

      // You decide to register for the loyalty card scheme on the website - apparently you
      // get a free popcorn for every 10 times you swipe your card.

      try
      {
        // Print xConnect if configuration is valid
        var arr = new[]
        {
                        @"  ____            _     _               ",
                        @"  |  _ \ ___  __ _(_)___| |_ ___ _ __   ",
                        @"  | |_) / _ \/ _` | / __| __/ _ \ '__|  ",
                        @"  |  _ <  __/ (_| | \__ \ ||  __/ |     ",
                        @"  |_| \_\___|\__, |_|___/\__\___|_|     ",
                        @"             |___/                      ",
                        };

        System.Console.WindowWidth = 160;
        foreach (string line in arr)
        {
          System.Console.WriteLine(line);
        }
        System.Console.WriteLine(); // Extra space
      }
      catch (Exception ex)
      {
        System.Console.WriteLine("Error: " + ex.Message);
        return "";
      }

      try
      {
        System.Console.WriteLine("We will generate an ID for you and print it onto a cool card!");

        System.Console.WriteLine("What is your first name?");

        var firstname = System.Console.ReadLine();

        System.Console.WriteLine("What is your last name?");

        var lastname = System.Console.ReadLine();

        PersonalInformation personalInfo = new PersonalInformation()
        {
          FirstName = firstname,
          LastName = lastname
        };

        System.Console.WriteLine("Favorite movie?");

        var favouriteMovie = System.Console.ReadLine();

        var registerInteraction = new RegisterInteraction(firstname, lastname, favouriteMovie);
        await registerInteraction.ExecuteInteraction();

        System.Console.WriteLine("Alright, your ID is - drumroll please... - " + registerInteraction.Identifier + "! Congratulations. Ctrl +c, Ctrl+v that number into your brain");
        System.Console.Write("Great! See you at the cinema. :) Press any key to continue.");
        System.Console.ReadKey();
      }
      catch (XdbExecutionException ex)
      {
        System.Console.WriteLine(ex.Message);
      }

      return Identifier;
    }
  }
}