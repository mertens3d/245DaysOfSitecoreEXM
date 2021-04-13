//using Shared.Helpers;
//using Shared.Interfaces;
//using Shared.Models;
//using Sitecore.XConnect;
//using Sitecore.XConnect.Client;
//using SitecoreCinema.Model.Collection;
//using System;
//using System.Threading.Tasks;

//namespace Console.Journey.Steps
//{
//  public class ReportKnownData
//  {
//    public IFeedback Feedback { get; }

//    public ReportKnownData(IFeedback feedback)
//    {
//      Feedback = feedback;
//    }

//    public void ReportSuccessMessage(string[] arr)
//    {
//      // Print xConnect if configuration is valid
//      Feedback.WindowWidth(160);

//      foreach (string line in arr)
//      {
//        Feedback.WriteLine(line);
//      }
//      Feedback.WriteLine(""); // Extra space
//    }

//    public async Task ReportAsync(XConnectClient client, Contact contact)
//    {
//      try
//      {


//        if (contactAgain != null)
//        {
//          System.Console.WriteLine(string.Format("Contact id: " + contactAgain.Id));
//          System.Console.WriteLine(string.Format("Your name is {0} {1} and your favorite movie is {2}", contactAgain.details.FirstName, contactAgain.details.LastName, contactAgain.movie.FavoriteMovie));

//          System.Console.WriteLine("Today you have had " + contactAgain.Interactions.Count + " interactions with us");

//          var i = 0;

//          foreach (var interactionsToday in contactAgain.Interactions)
//          {
//            i++;
//            var cinemaId = interactionsToday.GetFacet<CinemaInfo>(CinemaInfo.DefaultFacetKey);
//            System.Console.WriteLine("");

//            System.Console.WriteLine("Interaction #" + i + (cinemaId != null ? " at Cinema #" + cinemaId.CinimaId : string.Empty));

//            System.Console.Write("Events: ");

//            foreach (var evv in interactionsToday.Events)
//            {
//              System.Console.Write(evv.GetType().ToString() + "");
//            };
//            System.Console.WriteLine("");
//          }
//        }
//      }
//      catch (Exception ex)
//      {
//        System.Console.WriteLine(ex.Message);
//        throw;
//      }
//      System.Console.ReadKey();
//    }
//  }
//}