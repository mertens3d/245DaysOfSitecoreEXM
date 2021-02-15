using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using SitecoreCinema.Model.Collection;
using System;
using System.Threading.Tasks;

namespace Console.Journey.Interactions
{
  public class ReportContactData
  {
    public async Task ReportAsync(XConnectClient client, Contact contact)
    {
      try
      {
        System.Console.WriteLine("Before you go - do you want to see the data we collected about you today? :)");

        var contactAgain = await client.GetAsync<Contact>(contact, new ContactExpandOptions(PersonalInformation.DefaultFacetKey, CinemaVisitorInfo.DefaultFacetKey)
        {
          Interactions = new RelatedInteractionsExpandOptions(new string[]
          {
          CinemaInfo.DefaultFacetKey
          })
          {
            StartDateTime = DateTime.Today
          }
        });

        if (contactAgain != null)
        {
          var details = contactAgain.GetFacet<PersonalInformation>();
          var movie = contactAgain.GetFacet<CinemaVisitorInfo>();

          System.Console.WriteLine(String.Format("Contact id: " + contactAgain.Id));
          System.Console.WriteLine(String.Format("Your name is {0} {1} and your favorite movie is {2}", details.FirstName, details.LastName, movie.FavoriteMovie));

          System.Console.WriteLine("Today you have had " + contactAgain.Interactions.Count + " interactions with us");

          var i = 0;

          foreach (var interactionsToday in contactAgain.Interactions)
          {
            i++;
            var cinemaId = interactionsToday.GetFacet<CinemaInfo>(CinemaInfo.DefaultFacetKey);
            System.Console.WriteLine("");

            System.Console.WriteLine("Interaction #" + i + (cinemaId != null ? " at Cinema #" + cinemaId.CinimaId : string.Empty));

            System.Console.Write("Events: ");

            foreach (var evv in interactionsToday.Events)
            {
              System.Console.Write(evv.GetType().ToString() + "");
            };
            System.Console.WriteLine("");
          }
        }
      }
      catch (Exception ex)
      {
        System.Console.WriteLine(ex.Message);
        throw;
      }
      System.Console.ReadKey();
    }
  }
}