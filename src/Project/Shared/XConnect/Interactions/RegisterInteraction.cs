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
      XConnectContact = new Contact(new ContactIdentifier[] { contactIdentifier });

      PersonalInformation personalInfo = new PersonalInformation()
      {
        FirstName = this.FirstName,
        LastName = this.LastName
      };

      CinemaVisitorInfo visitorInfo = new CinemaVisitorInfo()
      {
        FavoriteMovie = this.FavoriteMovie
      };

      Client.AddContact(XConnectContact);
      Client.SetFacet<CinemaVisitorInfo>(XConnectContact, CinemaVisitorInfo.DefaultFacetKey, visitorInfo);
      Client.SetFacet<PersonalInformation>(XConnectContact, PersonalInformation.DefaultFacetKey, personalInfo);

      Interaction interaction = new Interaction(XConnectContact, InteractionInitiator.Brand, Const.XConnect.Channels.RegisterInteractionCode, string.Empty);

      interaction.Events.Add(new Goal(Const.XConnect.Goals.RegistrationGoal, DateTime.UtcNow));

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