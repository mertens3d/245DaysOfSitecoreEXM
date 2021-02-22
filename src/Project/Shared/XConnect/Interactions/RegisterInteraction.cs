using Shared.Models.SitecoreCinema;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using System;

namespace Shared.XConnect.Interactions
{
  public class RegisterInteraction : _interactionBase
  {
    public RegisterInteraction(string firstName, string lastName, string favoriteMovie, string userId = "") : base("")
    {
      FirstName = firstName;
      LastName = lastName;
      FavoriteMovie = favoriteMovie;
      UserId = userId;
    }

    private string FirstName { get; }
    private string LastName { get; }
    private string FavoriteMovie { get; }
    public string UserId { get; set; }

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

      // Initialize a client using the validate configuration

      if (string.IsNullOrEmpty(UserId))
      {
        UserId = "L94564543543543534" + Guid.NewGuid();
      }

      ContactIdentifier contactIdentifier = new ContactIdentifier(Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, UserId, ContactIdentifierType.Known);

      // Let's save this for later
      Identifier = contactIdentifier.Identifier;
      Contact = new Contact(new ContactIdentifier[] { contactIdentifier });

      PersonalInformation personalInfo = new PersonalInformation()
      {
        FirstName = this.FirstName,
        LastName = this.LastName
      };

      CinemaVisitorInfo visitorInfo = new CinemaVisitorInfo()
      {
        FavoriteMovie = this.FavoriteMovie
      };

      Client.AddContact(Contact);
      Client.SetFacet<CinemaVisitorInfo>(Contact, CinemaVisitorInfo.DefaultFacetKey, visitorInfo);
      Client.SetFacet<PersonalInformation>(Contact, PersonalInformation.DefaultFacetKey, personalInfo);

      var offlineChannel = Guid.NewGuid();
      var registrationGoalId = Guid.NewGuid();

      Interaction interaction = new Interaction(Contact, InteractionInitiator.Brand, offlineChannel, string.Empty);

      interaction.Events.Add(new Goal(registrationGoalId, DateTime.UtcNow));

      try
      {
        Client.AddInteraction(interaction);
        await Client.SubmitAsync();
      }
      catch (Exception ex)
      {
        Errors.Add(ex.Message);
      }
    }
  }
}