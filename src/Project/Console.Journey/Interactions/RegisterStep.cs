using Shared;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using SitecoreCinema.Model.Collection;
using System;
using System.Threading.Tasks;

namespace Console.Journey.JourneySteps
{
  public class RegisterStep
  {
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

      var cfgGenerator = new CFGGenerator();
      string Identifier = "";
      var cfg = cfgGenerator.GetCFG(SitecoreCinemaModel.Model);

      try
      {
        await cfg.InitializeAsync();

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
          System.Console.WriteLine(line);
        System.Console.WriteLine(); // Extra space
      }
      catch (Exception ex)
      {
        System.Console.WriteLine("Error: " + ex.Message);
        return "";
      }

      // Initialize a client using the validate configuration
      using (var client = new XConnectClient(cfg))
      {
        try
        {
          ContactIdentifier identifier = new ContactIdentifier(Shared.Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema,
         "L94564543543543534" + Guid.NewGuid(), ContactIdentifierType.Known);

          System.Console.WriteLine("We will generate an ID for you and print it onto a cool card!");

          System.Console.WriteLine("Alright, your ID is - drumroll please... - " + identifier.Identifier + "! Congratulations. Ctrl +c, Ctrl+v that number into your brain");

          // Let's save this for later
          Identifier = identifier.Identifier;
          Contact contact = new Contact(new ContactIdentifier[] { identifier });

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

          CinemaVisitorInfo visitorInfo = new CinemaVisitorInfo()
          {
            FavoriteMovie = favouriteMovie
          };

          client.AddContact(contact);
          client.SetFacet<CinemaVisitorInfo>(contact, CinemaVisitorInfo.DefaultFacetKey, visitorInfo);
          client.SetFacet<PersonalInformation>(contact, PersonalInformation.DefaultFacetKey, personalInfo);

          var offlineChannel = Guid.NewGuid();
          var registrationGoalId = Guid.NewGuid();

          Interaction interaction = new Interaction(contact, InteractionInitiator.Brand, offlineChannel, string.Empty);

          interaction.Events.Add(new Goal(registrationGoalId, DateTime.UtcNow));

          client.AddInteraction(interaction);

          await client.SubmitAsync();

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
}