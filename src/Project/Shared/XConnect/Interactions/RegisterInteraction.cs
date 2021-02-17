using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using SitecoreCinema.Model.Collection;
using System;

namespace Shared.XConnect.Interactions
{
  public class RegisterInteraction : _xconnectBase
  {
    public RegisterInteraction(string firstName, string lastName, string favoriteMovie) : base("")
    {
      FirstName = firstName;
      LastName = lastName;
      FavoriteMovie = favoriteMovie;
    }

    private string FirstName { get; }
    private string LastName { get; }
    private string FavoriteMovie { get; }

    public override async void InteractionBody()
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
      var cfg = cfgGenerator.GetCFG(SitecoreCinemaModel.Model);

      try
      {
        await cfg.InitializeAsync();
      }
      catch (Exception ex)
      {
        Errors.Add(ex.Message);
      }

      // Initialize a client using the validate configuration
      using (var client = new XConnectClient(cfg))
      {
        try
        {
          ContactIdentifier identifier = new ContactIdentifier(Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema,
         "L94564543543543534" + Guid.NewGuid(), ContactIdentifierType.Known);

          // Let's save this for later
          Identifier = identifier.Identifier;
          Contact contact = new Contact(new ContactIdentifier[] { identifier });

          PersonalInformation personalInfo = new PersonalInformation()
          {
            FirstName = FirstName,
            LastName = LastName
          };

          CinemaVisitorInfo visitorInfo = new CinemaVisitorInfo()
          {
            FavoriteMovie = FavoriteMovie
          };

          client.AddContact(contact);
          client.SetFacet(contact, CinemaVisitorInfo.DefaultFacetKey, visitorInfo);
          client.SetFacet(contact, PersonalInformation.DefaultFacetKey, personalInfo);

          var offlineChannel = Guid.NewGuid();
          var registrationGoalId = Guid.NewGuid();

          Interaction interaction = new Interaction(contact, InteractionInitiator.Brand, offlineChannel, string.Empty);

          interaction.Events.Add(new Goal(registrationGoalId, DateTime.UtcNow));

          client.AddInteraction(interaction);

          await client.SubmitAsync();
        }
        catch (XdbExecutionException ex)
        {
          Errors.Add(ex.Message);
        }
      }
    }
  }
}