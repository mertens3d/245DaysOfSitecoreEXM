using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using SitecoreCinema.Model.Collection;
using System;
using System.Threading.Tasks;

namespace Console.Journey.JourneySteps
{
  public abstract class _interactionBase
  {
    public _interactionBase(string identifier)
    {
      this.Identifier = identifier;
    }

    protected PersonalInformation PersonalInfo { get; set; } = null;
    protected CinemaVisitorInfo CinemaInfo { get; set; } = null;
    protected XConnectClient Client { get; set; }
    protected Contact Contact { get; set; } = null;
    public string Identifier { get; }
    protected IdentifiedContactReference IdentifiedContactReference { get; set; }

    protected void DrawTriggerMessage(string message)
    {
      System.Console.ForegroundColor = ConsoleColor.Cyan;
      System.Console.WriteLine(message);

      System.Console.ForegroundColor = ConsoleColor.White;
    }

    protected void DrawPostInteractionMessage(string message)
    {
      DrawPostInteractionMessage(new string[] { message });
    }

    protected void DrawPostInteractionMessage(string[] messages)
    {
      foreach (var message in messages)
      {
        System.Console.WriteLine(message);
      }

      System.Console.WriteLine("Press any key to continue");
      System.Console.ReadKey();
    }

    protected async Task PopulateContactDataAsync()
    {
      if (Identifier != null)
      {
        IdentifiedContactReference = new IdentifiedContactReference(Shared.Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, Identifier);

        System.Console.WriteLine("Identifier: " + Identifier);

        if (Client != null)
        {
          Contact = await Client.GetAsync<Contact>(IdentifiedContactReference, new ContactExpandOptions(PersonalInformation.DefaultFacetKey, CinemaVisitorInfo.DefaultFacetKey));
          if (Contact != null)
          {
            PersonalInfo = Contact.GetFacet<PersonalInformation>();
            CinemaInfo = Contact.GetFacet<CinemaVisitorInfo>();
          }
        }
        else
        {
          System.Console.WriteLine("client was null");
        }
      }
      else
      {
        System.Console.WriteLine("Identifier was null");
      }
    }
  }
}