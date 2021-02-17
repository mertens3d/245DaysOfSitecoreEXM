using Console.Journey.Interactions;
using Shared.Interfaces;
using Shared.Models;
using Shared.XConnect.Interactions;
using SitecoreCinema.Model.Collection;
using System;
using System.Threading.Tasks;

namespace Shared.Interactions
{
  public class ReportContactDataStep : _journeyStepBase
  {
    public ReportContactDataStep(IFeedback feedback, string identifier) : base(feedback, identifier)
    {
    }

    public async Task ReportAsync()
    {
      try
      {
        Feedback.WriteLine("Before you go - do you want to see the data we collected about you today? :)");

        var knownDataReport = new ReportKnownData(Identifier);
        await knownDataReport.ExecuteInteraction();
        if (knownDataReport != null)
        {
          Feedback.WriteLine(string.Format("Contact id: " + knownDataReport.KnownData.Id));
          Feedback.WriteLine(string.Format("Your name is {0} {1} and your favorite movie is {2}", 
            knownDataReport.KnownData.details.FirstName, 
            knownDataReport.KnownData.details.LastName,
            knownDataReport.KnownData.movie.FavoriteMovie));

          Feedback.WriteLine("Today you have had " + knownDataReport.KnownData.Interactions.Count + " interactions with us");

          var i = 0;

          foreach (var interactionsToday in knownDataReport.KnownData.Interactions)
          {
            i++;
            var cinemaId = interactionsToday.GetFacet<CinemaInfo>(CinemaInfo.DefaultFacetKey);
            Feedback.WriteLine("");

            Feedback.WriteLine("Interaction #" + i + (cinemaId != null ? " at Cinema #" + cinemaId.CinimaId : string.Empty));

            Feedback.WriteLine("Events: ");

            foreach (var evv in interactionsToday.Events)
            {
              Feedback.WriteLine(evv.GetType().ToString() + "");
            };
            Feedback.WriteLine("");
          }
        }
      }
      catch (Exception ex)
      {
        Feedback.WriteLine(ex.Message);
        throw;
      }
      Feedback.ReadKey();
    }
  }
}